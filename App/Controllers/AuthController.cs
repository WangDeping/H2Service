
using Abp.Web.Models;
using H2Service.Authorization;
using H2Service.Authorization.Dto;
using H2Service.Extensions;
using H2Service.Users;
using H2Service.WeChatWork;
using H2Service.WeChatWork.Dto;
using System;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AuthController : AppControllerBase
    {
        

        private readonly IWxAppService _wxAppService;
        private readonly IUserAppService _userAppService;
        private readonly ILoginAppService _loginAppService;
        private readonly IAuthorizationManager _authorizationManager;

        public AuthController(IWxAppService wxAppService,
            IUserAppService userAppService,
            ILoginAppService loginAppService,
            IAuthorizationManager authorizationManager) {
            _wxAppService = wxAppService;
            _userAppService = userAppService;
            _loginAppService = loginAppService;
            _authorizationManager = authorizationManager;
        }

        public ActionResult Index()
        {          
            Session["retUrl"] = Request.QueryString["ReturnUrl"];  
            string authUrl = _wxAppService.GetWxAuthUrl();           
            Response.StatusCode = 301;
            Response.Status = "301 Moved Permanently";
            Response.AppendHeader("Location", authUrl);
            Response.AppendHeader("Cache-Control", "no-cache");
            Response.End();
            return View();
        }
       
        public ActionResult GetWxCode() {
            var code = Request.QueryString["code"]; 
            WxUserInfoBaseDto userBaseInfo = _wxAppService.GetWxUserBaseInfoByCode(code);
            //属于企业微信内部人员
            if (userBaseInfo.errcode == 0) {                
                if (!string.IsNullOrEmpty(userBaseInfo.OpenId)) { 
                    Logger.Error(string.Format("未注册用户登录失败{0},设备{1}", userBaseInfo.OpenId, userBaseInfo.DeviceId));
                    //throw new UserFriendlyException("非本院职工不能访问本系统");
                    return View("Denied", new ErrorInfo{  Details="非本院职工不能访问本系统"});
                }
                var user= _userAppService.GetUserByNumber(userBaseInfo.UserId);
                Logger.Error("用户登录："+user.UserNumber);
                //登入
                if (user != null)
                {
                    var userInput = ObjectMapper.Map<SignInInput>(user);  
                    _loginAppService.SignIn(userInput);
                }
            }
            ViewBag.UserName = AbpSession.GetUserName();
            if(!string.IsNullOrEmpty(Session["retUrl"]?.ToString()))           
                Response.Redirect(Session["retUrl"].ToString());
            return View();            
        }
        [Authorize]
        public ActionResult Test()
        {

            var userNumber = AbpSession.GetUserNumber();          
            var wxUser = _wxAppService.GetWxUserInfoDetail(userNumber);           
            ViewBag.User = wxUser;
              
            return View();
        }
        public ActionResult LoginOut()
        {

            _loginAppService.SignOut();

            return View();
        }
    }
}