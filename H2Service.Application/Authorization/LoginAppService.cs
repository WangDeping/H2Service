using Abp.UI;
using H2Service.Account;
using H2Service.Account.Dto;
using H2Service.Authorization.Dto;
using H2Service.Extensions;
using H2Service.Users;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading;
using System.Web;

namespace H2Service.Authorization
{
    public   class LoginAppService:H2ServiceAppServiceBase,ILoginAppService
    {
        private IAuthenticationManager _authenticationManager;
        private IUserAppService _userAppService;
        private readonly WxSender _wxSender;
       // private IAuthenticationManager _authenticationManager => HttpContext.Current.GetOwinContext().Authentication;
        public LoginAppService(IAuthenticationManager authenticationManager,
            IUserAppService userAppService,
            WxSender wxSender)
        {
            _authenticationManager = authenticationManager;
            _userAppService = userAppService;
            _wxSender = wxSender;
        }

        /// <summary>
        /// 用户登入
        /// </summary>
        /// <param name="user"></param>
        /// <param name="identity"></param>
        public void SignIn(SignInInput user,ClaimsIdentity identity=null)
        {
            if (identity == null)
            {
                var department = _userAppService.GetDefaultDepartment(user.Id);
                if (department == null)
                {
                    department = new DepartmentDto { Id = 0, DepartmentName = "未分配" };
                }
                identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
                identity.AddClaim(new Claim("UserNumber", user.UserNumber));
                identity.AddClaim(new Claim("DepartmentId",department.Id.ToString()));
                identity.AddClaim(new Claim("DepartmentName", department.DepartmentName));           
               
                //identity.AddClaim(new Claim("AvatarUrl", user.AvatarUrl==null?"": user.AvatarUrl));                
            }
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);           
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);            
        }

        public void SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public void SendValidateCode(string userNumber,string validateCode)
        {
            var user = _userAppService.GetUserByNumber(userNumber);
            var content = string.Format("验证码为:{0},请勿告诉他人，验证码有效期5分钟", validateCode);         
            var sendMsg = new WxSendTextMsg(content,user.UserNumber);
            _wxSender.SendMsg(sendMsg);
        }
    }
}
