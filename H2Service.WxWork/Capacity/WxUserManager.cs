using Abp.Dependency;
using Castle.Core.Logging;
using H2Service.WxWork.Entities;
using H2Service.WxWork.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Configuration;

namespace H2Service.WxWork
{
    public   class WxUserManager: ITransientDependency
    {
        private const string DELETE_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}";
        private const string CREATE_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}";
        private const string GETUSERBASE_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}";
        private const string GETAUTHUSER_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}";
        private const string GETUSERS_URL = "https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child=1";


        private WxTokenManager _tokenManager;
        private ILogger _logger;
        private string token;
        public WxUserManager(ILogger logger,
            WxTokenManager wxTokenManager)
        {
            _tokenManager = wxTokenManager;
            _logger = logger;
            token = _tokenManager.GetWxToken(WebConfigurationManager.AppSettings["contactsAppid"]);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userid">系统的UserNumber,微信端的userid</param>
        /// <returns></returns>
        public WxGetUserBaseInfo GetWxUserBaseInfo(string userid)
        {         
            string wxUrl = string.Format(GETUSERBASE_URL, token, userid);
           var retUserDetail= GetFromWxUrl<WxGetUserBaseInfo>(wxUrl);
            if (retUserDetail.errcode == 60111)
                    _logger.Error("需要删除用户:" + userid);//_userManager.DeleteUser(UserNumber);  
            return retUserDetail;

        }

        /// <summary>
        /// 获取验证url
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        public string GetWxAuthUrl(string scope = "snsapi_base")
        {
            string wxUrl = "";
            string appid = WebConfigurationManager.AppSettings["corpid"];
            var authAppid = WebConfigurationManager.AppSettings["authAppid"];
            string authUrl = WebConfigurationManager.AppSettings["authUrl"];
            wxUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&" +
                    "redirect_uri={1}&response_type=code&scope={2}&" +
                    "agentid={3}&state=STATE&connect_redirect=1#wechat_redirect", appid, authUrl, scope, authAppid);

            return wxUrl;
        }
        /// <summary>
        /// 获取根据code验证用户是否为本单位用户
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public WxAuthUserInfo GetWxAuthUserInfo(string code)
        {         
            string wxUrl = string.Format(GETAUTHUSER_URL, token, code);
            return GetFromWxUrl<WxAuthUserInfo>(wxUrl);
        }

        /// <summary>
        /// 获取部门下所有成员详情
        /// </summary>
        /// <param name="deptId">部门Id，1代表全院</param>
        /// <returns></returns>
        public List<WxGetUserInfo> GetWxUsersByDeptId(string deptId = "1")
        {           
            string wxUrl = string.Format(GETUSERS_URL, token, deptId);
            var ret = this.GetFromWxUrl<WxUserInfoList>(wxUrl);           
            return ret.userlist;
        }

        public void Create(WxCreateUserInfo user)
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

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userNumber"></param>
        public void Delete(string userNumber)
        {

            var postUrl = string.Format(DELETE_URL, token, userNumber);

            using (var client = new HttpClient())
            {
                var result = client.GetAsync(postUrl);
                var responseJson = result.Result.Content.ReadAsStringAsync().Result;
                _logger.Error("删除用户" + userNumber + responseJson);
            }
        }

        private T GetFromWxUrl<T>(string wxUrl) where T : WxRetBase
        {
            var result = Helper.GetURLResultByUTF8(wxUrl);
            var retAuthUser = Helper.JsonDeserialize<T>(result);
            if (retAuthUser.errcode != 0 && retAuthUser.errcode != 40029)
            {               
                _logger.Error(result);
                throw new Exception("微信错误代码:" + result);
            }
            return retAuthUser;

        }

    }
}
