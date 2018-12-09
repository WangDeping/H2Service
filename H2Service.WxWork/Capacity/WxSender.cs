using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2Service.WxWork.Entities;
using Abp.Domain.Services;
using System.Net.Http;
using Newtonsoft.Json;
using Abp.Dependency;
using Castle.Core.Logging;

namespace H2Service.WxWork
{
    /// <summary>
    /// 发送消息
    /// </summary>
    public class WxSender : ITransientDependency
    {
        private WxTokenManager _tokenManager;
        private ILogger _logger;
        public WxSender(WxTokenManager wxTokenManager,
            ILogger logger)
        {
            _tokenManager = wxTokenManager;
            _logger = logger;
        }
        private const string SENDMSG_URL= "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        public WxRetMsg SendMsg<T>(T msg)where T:WxSendMsg
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

        public async Task<WxRetMsg> SendMsgAsync<T>(T msg)where T:WxSendMsg
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

    
    }
}
