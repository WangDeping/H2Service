using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2Service.WeChatWork.Entities;
using Abp.Domain.Services;
using System.Net.Http;
using Newtonsoft.Json;

namespace H2Service.WeChatWork
{
    public class WxSender : DomainService
    {
        private WxTokenManager _tokenManager;
        public WxSender(WxTokenManager wxTokenManager)
        {
            _tokenManager = wxTokenManager;
        }
        private const string SENDMSG_URL= "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        public WxRetMsg SendMsg(WxSendMsg msg)
        {
            var access_token = this._tokenManager.GetWxToken(msg.agentid);
            var postUrl = string.Format(SENDMSG_URL, access_token);

            using (var client = new HttpClient())
            {
                var postContent = new StringContent(JsonConvert.SerializeObject(msg));
                var result = client.PostAsync(postUrl, postContent);               
                var responseJson = result.Result.Content.ReadAsStringAsync().Result;
              
                return JsonConvert.DeserializeObject<WxRetMsg>(responseJson);

            }
        }

        public async Task<WxRetMsg> SendMsgAsync(WxSendMsg msg)
        {
            var access_token = this._tokenManager.GetWxToken(msg.agentid);
           var postUrl= string.Format(SENDMSG_URL, access_token);
            using (var client = new HttpClient())
            {
                var postContent = new StringContent(JsonConvert.SerializeObject(msg));
                var task = client.PostAsync(postUrl, postContent);
                task.Result.EnsureSuccessStatusCode();
                var responseJson = await task.Result.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WxRetMsg>(responseJson);

            }
        }

       /* public WxRetAuthBase GetAuthBaseByCode(string code)
        {
            var agentid = "0";
            var urlencode = "http%3a%2f%2flocalhost%3a8000%2fSalary%2fIndex";
            var accessToken = _tokenManager.GetWxToken("0");
            var codeUrl = "https://open.weixin.qq.com/connect/oauth2/authorize?appid=wxbf27fd9188f6ef48&redirect_uri=http%3a%2f%2fdyoa.dysdermyy.org%3a8000%2f&response_type=code&scope=SCOPE&agentid=0&state=STATE#wechat_redirect";

            //var postWxUrl = " https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token=ACCESS_TOKEN&code=CODE";
        }*/
    }
}
