
using Abp.Web.Models;
using H2Service.Authorization;
using H2Service.Authorization.Dto;
using H2Service.Extensions;
using H2Service.Log4;
using H2Service.Users;
using H2Service.WxWork;
using System;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AuthController : AppControllerBase
    {
        

        private readonly WxAuthManager _wxAuthManager;
        private readonly IUserAppService _userAppService;
        private readonly ILoginAppService _loginAppService;
        private readonly IAuthorizationManager _authorizationManager;       
        public AuthController(WxAuthManager wxAuthManager,
            IUserAppService userAppService,
            ILoginAppService loginAppService,
            IAuthorizationManager authorizationManager
           ) {
            _wxAuthManager = wxAuthManager;
            _userAppService = userAppService;
            _loginAppService = loginAppService;
            _authorizationManager = authorizationManager;
        }

        public ActionResult Index()
        {          
            Session["retUrl"] = Request.QueryString["ReturnUrl"];  
            string authUrl = _wxAuthManager.GetWxAuthUrl();           
            Response.StatusCode = 301;
            Response.Status = "301 Moved Permanently";
            Response.AppendHeader("Location", authUrl);
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.End();
            return View();
        }
       
        public ActionResult GetWxCode() {
            var code = Request.QueryString["code"];
            var authUserInfo = _wxAuthManager.GetWxAuthUserInfo(code);
            //属于企业微信内部人员
            if (authUserInfo.errcode == 0) {                
                if (!string.IsNullOrEmpty(authUserInfo.OpenId)) { 
                    Logger.Error(string.Format("未注册用户登录失败{0},设备{1}", authUserInfo.OpenId, authUserInfo.DeviceId));
                    return View("Denied", new ErrorInfo{  Details="非本院职工不能访问本系统"});
                }
                var user= _userAppService.GetUserByNumber(authUserInfo.UserId);              
                //登入
                if (user != null)
                {
                    var userInput = ObjectMapper.Map<SignInInput>(user);
                    _loginAppService.MobileLoginLog(authUserInfo.UserId, authUserInfo.DeviceId);
                    _loginAppService.SignIn(userInput);                   
                }
                else
                    return View("Denied", new ErrorInfo { Details = "非本院职工不能访问本系统" });
            }         
            if(!string.IsNullOrEmpty(Session["retUrl"]?.ToString()))           
                Response.Redirect(Session["retUrl"].ToString());
            return View();            
        }
      
        public ActionResult LoginOut()
        {

            _loginAppService.SignOut();

            return View();
        }

        public string AuthWx(string msg_signature, string timestamp, string nonce, string echostr)
        {
            Logger.Error(msg_signature+"+"+timestamp+"+"+nonce+"+"+echostr);
          return  _wxAuthManager.AuthWx(msg_signature, timestamp, nonce, echostr);
        }
    }
}