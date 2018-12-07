using Abp.Dependency;
using Abp.Domain.Services;
using Castle.Core.Logging;
using H2Service.Authorization;
using H2Service.WeChatWork.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.WeChatWork
{
 public   class WxUserManager: ITransientDependency
    {
        private WxTokenManager _tokenManager;
        private ILogger _logger;
        public WxUserManager(ILogger logger,
            WxTokenManager wxTokenManager)
        {
            _tokenManager = wxTokenManager;
            _logger = logger;
        }
        private const string DELETE_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";
        private const string CREATE_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}"
;        public void Delete(string userNumber)
        {
            var concatId = WebConfigurationManager.AppSettings["contactsAppid"];
            var access_token = this._tokenManager.GetWxToken(concatId);
            var postUrl = string.Format(DELETE_URL, access_token,userNumber);
            
            using (var client = new HttpClient())
            {               
                var result = client.GetAsync(postUrl);
                var responseJson = result.Result.Content.ReadAsStringAsync().Result;
                _logger.Error("删除用户" + userNumber+responseJson);
            }
        }

        public void Create(WxUserInfo user)
        {
            var concatId = WebConfigurationManager.AppSettings["contactsAppid"];
            var access_token = this._tokenManager.GetWxToken(concatId);
            var postUrl = string.Format(CREATE_URL, access_token);

            using (var client = new HttpClient())
            {
                var postUser = new StringContent(JsonConvert.SerializeObject(user));
                var result = client.PostAsync(postUrl, postUser);
                var responseJson = result.Result.Content.ReadAsStringAsync().Result;            
              
                _logger.Error("添加用户" + user.name +"头像"+user.avatar_mediaid+ responseJson);
            }
        }
    }
}
