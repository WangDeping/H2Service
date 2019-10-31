using Abp.Dependency;
using Castle.Core.Logging;
using H2Service.WxWork.Entities;
using H2Service.WxWork.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace H2Service.WxWork
{
 public   class WxAuthManager : ITransientDependency
    {

        private const string GETAUTHUSER_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}";
        private const string GETAUTHURL_URL = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&agentid={3}&state=STATE&connect_redirect=1#wechat_redirect";
        private string sToken = "kDncXp8dPVNvbALV";
        private string sEncodingAESKey = "uSyVcn9Fc3BqUCM6PLjrjrwHl36smHObrszD2k9XukG";
        private readonly string corpID = "";
        private WxTokenManager _tokenManager;
        private ILogger _logger;
        private string token = "";
        public WxAuthManager(ILogger logger,
            WxTokenManager wxTokenManager)
        {
            _tokenManager = wxTokenManager;
            _logger = logger;
            corpID = WebConfigurationManager.AppSettings["corpid"];


        }
        /// <summary>
        /// 构建验证url
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public string GetWxAuthUrl(string scope = "snsapi_base")
        {
            
            string appid = WebConfigurationManager.AppSettings["corpid"];
            var authAppid = WebConfigurationManager.AppSettings["authAppid"];
            string authUrl =HttpUtility.UrlEncode(WebConfigurationManager.AppSettings["authUrl"]);
            var wxUrl = string.Format(GETAUTHURL_URL, appid, authUrl, scope, authAppid);

            return wxUrl;
        }
        /// <summary>
        /// 获取根据code验证用户是否为本单位用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WxAuthUserInfo GetWxAuthUserInfo(string code)
        {          
            token = _tokenManager.GetWxToken(WebConfigurationManager.AppSettings["authAppid"]);
            string wxUrl = string.Format(GETAUTHUSER_URL, token, code);
            var result = Helper.GetURLResultByUTF8(wxUrl);
            var retAuthUser = Helper.JsonDeserialize<WxAuthUserInfo>(result);
            if (retAuthUser.errcode != 0 && retAuthUser.errcode != 40029)
            {
                _logger.Error(result);
                throw new Exception("微信错误代码:" + result);
            }
            return retAuthUser;
         
        }

        public string AuthWx(string msg_signature, string timestamp, string nonce, string echostr)
        {


            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(sToken, sEncodingAESKey, corpID);
            string sVerifyMsgSig = msg_signature;
            string sVerifyTimeStamp = timestamp;
            string sVerifyNonce = nonce;
            string sVerifyEchoStr = echostr;

            int ret = 0;
            string sEchoStr = "";
            ret = wxcpt.VerifyURL(sVerifyMsgSig, sVerifyTimeStamp, sVerifyNonce, sVerifyEchoStr, ref sEchoStr);

            if (ret != 0)
            {
                _logger.Error("微信验证出错");
            }
            //ret==0表示验证成功，sEchoStr参数表示明文，用户需要将sEchoStr作为get请求的返回参数，返回给企业微信。
             //HttpUtils.SetResponse(sEchoStr);
            _logger.Info("微信验证成功");
            return sEchoStr;
        }
    }
}
