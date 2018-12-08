using Abp.Dependency;
using Abp.Domain.Services;
using Abp.Runtime.Caching;
using Abp.UI;
using Castle.Core.Logging;
using H2Service.WxWork.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Xml.Linq;

namespace H2Service.WxWork
{
 public  class WxTokenManager: ITransientDependency
    {
        private readonly string tokenPath;
        private readonly string jsApiTicketPath;
        private  ILogger logger;
        private readonly ICacheManager _cacheManager;
        private string sToken = "kDncXp8dPVNvbALV";
        private string sCorpID = "wxbf27fd9188f6ef48";
        private string sEncodingAESKey = "uSyVcn9Fc3BqUCM6PLjrjrwHl36smHObrszD2k9XukG";

        public WxTokenManager(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
            logger = NullLogger.Instance;
            if (HttpContext.Current != null)
            {
                tokenPath = HttpContext.Current.Server.MapPath(@"~/access_token.config");
                jsApiTicketPath= HttpContext.Current.Server.MapPath(@"~/js_ticket.config");
            }
            else
            {
                tokenPath = @AppDomain.CurrentDomain.BaseDirectory + "access_token.config";
                jsApiTicketPath= @AppDomain.CurrentDomain.BaseDirectory + "js_ticket.config";               
            }


        }
        /// <summary>
        /// 获取某个微信企业应用的accesstoken,所有token都保存到cache里，防止频率限制
        /// </summary>
        /// <param name="agentid">企业应用id</param>
        /// <returns></returns>
        internal string GetWxToken(string agentid)
        {

            XDocument xdocument = XDocument.Load(tokenPath);
            var app = xdocument.Root.Elements("app").Where(T => T.Element("agentid").Value == agentid).First();
            if (app == null)
                throw new Exception("应用id找不到");
           return _cacheManager.GetCache("WxTokenCache").Get(agentid, () =>
            {
                using (var client = new HttpClient())
                {
                    var result = client.GetStringAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", this.sCorpID, app.Element("secret").Value));
                    result.Wait();
                   var token = JsonConvert.DeserializeObject<WxAccessToken>(result.Result);               
                    return token.access_token;
                }

            });        

        }
        /// <summary>
        /// 向微信获取access_token，并且保存到本地xml
        /// </summary>
        /// <param name="agentid"></param>
        /// <returns></returns>
        private string RefreshWxToken(string agentid)
        {
            XDocument xdocument = XDocument.Load(tokenPath);
            var app = xdocument.Root.Elements("app").Where(T => T.Element("agentid").Value == agentid).First();
            if (app == null) {
                logger.Error("应用agentid找不到");
                throw new UserFriendlyException("应用id找不到");
            }
                
           
            var appsecret = app.Element("secret").Value;
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}",this.sCorpID, appsecret));
                result.Wait();
                var token = JsonConvert.DeserializeObject<WxAccessToken>(result.Result);
                if (token.errcode == 0)
                {
                    app.Element("access_token").Value = token.access_token;
                    app.Element("expires_in").Value = token.expires_in.ToString();
                    app.Element("last_update").Value = DateTime.Now.ToString();
                    xdocument.Save(tokenPath);
                }
                return token.access_token;
            }

        }

        public string GetWxJSApiTicket()
        {
            string tooken = GetWxToken(WebConfigurationManager.AppSettings["authAppid"]);           
            return _cacheManager.GetCache("WxJSTicketCache").Get("JsTicket", () =>
            {
                using (var client = new HttpClient())
                {
                    var result = client.GetStringAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", tooken));
                    result.Wait();
                    var ticket = JsonConvert.DeserializeObject<WxJSApiTicket>(result.Result);                 
                    return ticket.ticket;
                }

            });          

        }
        private string RefreshWxJSApiTicket()
        {
            string tooken = GetWxToken(WebConfigurationManager.AppSettings["authAppid"]);
            XDocument xdocument = XDocument.Load(jsApiTicketPath);      
            var node = xdocument.Root.Element("js_ticket");
            using (var client = new HttpClient())
            {
                var result = client.GetStringAsync(string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", tooken));
                result.Wait();
                var ticket = JsonConvert.DeserializeObject<WxJSApiTicket>(result.Result);
                if (ticket.errcode == 0)
                {
                    node.Element("ticket").Value =ticket.ticket;
                    node.Element("expires_in").Value = ticket.expires_in.ToString();
                    node.Element("last_update").Value = DateTime.Now.ToString();
                    xdocument.Save(jsApiTicketPath);
                }
                return ticket.ticket;
            }

        }
        public string AuthWx(string msg_signature, string timestamp, string nonce, string echostr)
        {


            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, sCorpID);
            string sVerifyMsgSig = msg_signature;
            string sVerifyTimeStamp = timestamp;
            string sVerifyNonce = nonce;
            string sVerifyEchoStr = echostr;

            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);

            if (ret != 0)
            {
                logger.Error("微信验证出错");
            }
            //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业微信。
            // HttpUtils.SetResponse(sEchoStr);
            logger.Info("微信验证成功");
            return sEchoStr;
        }
    }
}
