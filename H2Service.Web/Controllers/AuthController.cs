using Abp.Runtime.Caching;
using Abp.Web.Models;
using Abp.Web.Mvc.Controllers;
using H2Service.Authorization;
using H2Service.Authorization.Dto;
using H2Service.Extensions;
using H2Service.Users;
using H2Service.Web.Models.Users;
using H2Service.WxWork;
using System;
using System.Threading;
using System.Web.Configuration;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class AuthController : AbpController
    {
       
        private readonly IUserAppService _userAppService;
        private readonly ILoginAppService _loginAppService;
        private readonly IPermissionAppService _permissionAppService;
        private readonly IRoleAppService _roleAppService;
        private readonly ICacheManager _cacheManager;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly WxAuthManager _wxAuthManager;
        public AuthController(WxAuthManager wxAuthManager,
            IUserAppService userAppService,
            ILoginAppService loginAppService,
            IPermissionAppService permissionAppService,
            IRoleAppService roleAppService,
            ICacheManager cacheManager,
            IAuthorizationManager authorizationManager)
        {
            _wxAuthManager = wxAuthManager;
            _userAppService = userAppService;
            _loginAppService = loginAppService;
            _permissionAppService = permissionAppService;
            _roleAppService = roleAppService;
            _authorizationManager = authorizationManager;
            _cacheManager = cacheManager;
        }
        // GET: Auth
        public ActionResult Index()
        {
            Session["retUrl"] = Request.QueryString["ReturnUrl"];
            if (string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                Session["retUrl"] = "Index";

            ViewBag.AuthUrl = Server.UrlEncode(WebConfigurationManager.AppSettings["authUrl"]);
            ViewBag.authAppid = WebConfigurationManager.AppSettings["authAppid"];
            return View();
        }
  
        [DontWrapResult]
        public JsonResult Login(LoginViewModel loginViewModel)
        {            
            var retUrl = "";
            if(string.IsNullOrEmpty(loginViewModel.UserNumber)||string.IsNullOrEmpty(loginViewModel.Password))
                return Json(new { ErrMsg = "工号密码不能为空", ErrCode = -1 }, JsonRequestBehavior.AllowGet);
            var user = _userAppService.GetUserByNumberAndPassword(loginViewModel.UserNumber, loginViewModel.Password);

            if (user != null)
            {
                var signInput = ObjectMapper.Map<SignInInput>(user);
                _loginAppService.SignIn(signInput);//登入

                if (!string.IsNullOrEmpty(Session["retUrl"]?.ToString()))
                {
                    if (Session["retUrl"]?.ToString() == "Index")//如果直接从Auth/Index进入 
                        retUrl = "/Home/Index";
                    else
                        retUrl = Session["retUrl"].ToString();
                }
                else
                    retUrl = "/Home/Index";
                return Json(new  { ErrMsg = "登录成功", ErrCode = 0, RetUrl = retUrl }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { ErrMsg = "工号密码错误", ErrCode = -1}, JsonRequestBehavior.AllowGet);

        }
        [DontWrapResult]
        public JsonResult LoginValidate(LoginValidateModel model)
        {
            var retUrl = "";
            var cacheCode=_cacheManager.GetCache("LoginValidateCodeCache").GetOrDefault<string,string>(model.UserNumber);
            if (string.IsNullOrEmpty(cacheCode))
                return Json(new ErrorInfo(-2, "动态密码过期,请重新获取")); 
          
            if (model.ValidateCode ==cacheCode)
            {
                var user = _userAppService.GetUserByNumber(model.UserNumber);
                var signInput = ObjectMapper.Map<SignInInput>(user);
                _loginAppService.SignIn(signInput);              
                if (!string.IsNullOrEmpty(Session["retUrl"]?.ToString()))
                {
                    if (Session["retUrl"]?.ToString() == "Index")//如果直接从Auth/Index进入 
                        retUrl = "/Home/Index";
                    else
                        retUrl = Session["retUrl"].ToString();
                }
                else
                    retUrl = "/Home/Index";
                return Json(new ErrorInfo { Code=0, Message="登录成功", Details=retUrl });

            }
            else
                return Json(new ErrorInfo(-1, "动态密码不正确"));
        }

        public ActionResult GetWxCode()
        {
            var code = Request.QueryString["code"];

            var userAuthInfo = _wxAuthManager.GetWxAuthUserInfo(code);
            //属于企业微信内部人员
            if (userAuthInfo.errcode == 0)
            {
                var user = _userAppService.GetUserByNumber(userAuthInfo.UserId);
                //登入
                if (user != null)
                {
                    var signInput = ObjectMapper.Map<SignInInput>(user);                    
                    _loginAppService.SignIn(signInput);//登入                 
                }

            }
            ViewBag.UserName = AbpSession.GetUserName();
            if (!string.IsNullOrEmpty(Session["retUrl"]?.ToString()))
            {
                if (Session["retUrl"]?.ToString() == "Index")//如果直接从Auth/Index进入 
                    Response.Redirect("/Home/Index");
                else
                    Response.Redirect(Session["retUrl"].ToString());

            }

            return View();
        }
        [DontWrapResult]
        public JsonResult SendValidateCode(string userNumber)
        {
            Thread.Sleep(100);
            var code = new Random().Next(100000, 999999).ToString();
            try
            {
                _loginAppService.SendValidateCode(userNumber,code);
                _cacheManager.GetCache("LoginValidateCodeCache").Set(userNumber,code);
                return Json(new ErrorInfo(0, "请在企业小助手中查看动态密码"));
            }
            catch (Exception ex)
            {
                return Json(new ErrorInfo(-1, ex.Message));
            }
           

        }



        public ActionResult LoginOut()
        {
            _loginAppService.SignOut();
            return RedirectToAction("Index"); ;
        }

        [DontWrapResult]
        public JsonResult GetAllPermissions(AssignPermissionInput input)
        {
            var permission = _permissionAppService.GetAssignedPermissionTree(input);
            return Json(permission, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PermissionIndex()
        {
            return View();
        }


        public PartialViewResult PermissionModalPartialView()
        {
            return PartialView("PermissionModalPartialView");

        }




    }
}