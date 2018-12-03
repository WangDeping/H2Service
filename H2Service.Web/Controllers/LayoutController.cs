using Abp;
using Abp.Application.Navigation;
using Abp.Configuration.Startup;
using Abp.Localization;
using Abp.Runtime.Session;
using Abp.Threading;
using H2Service.Account;
using H2Service.Extensions;
using H2Service.Users;
using H2Service.Web.Models.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using H2Service.Account.Dto;

namespace H2Service.Web.Controllers
{
    [Authorize]
    public class LayoutController : H2ServiceControllerBase
    {
        private readonly IUserNavigationManager _userNavigationManager;       
        private readonly IMultiTenancyConfig _multiTenancyConfig;
        private readonly ILanguageManager _languageManager;
        private readonly IUserAppService _userAppService;      
        //private readonly ISessionAppService _sessionAppService;
        // GET: Layout
        public LayoutController(
             IUserNavigationManager userNavigationManager,
           //  ISessionAppService sessionAppService,
             IMultiTenancyConfig multiTenancyConfig,
             ILanguageManager languageManager,
             IUserAppService userAppService)
        {   _userNavigationManager = userNavigationManager;
           // _sessionAppService = sessionAppService;
            _multiTenancyConfig = multiTenancyConfig;
            _languageManager = languageManager;
            _userAppService = userAppService;
        }
        [ChildActionOnly]
        public PartialViewResult TopBarUserArea()
        {
            var user= _userAppService.GetUserById((int)AbpSession.UserId);
            user.AvatarUrl = string.IsNullOrEmpty(user.AvatarUrl) ?("default/avatar.png"):(user.UserNumber + ".jpg");
            var topBarViewModel = new TopBarViewBar { User = user,Department =new DepartmentDto {  Id=int.Parse(AbpSession.GetDepartmentId()), DepartmentName=AbpSession.GetDepartmentName() } };
            return PartialView("_TopBarUserArea", topBarViewModel);
        }

        [ChildActionOnly]
        public PartialViewResult SideBarNav(string activeMenu = "")
        {
            var model = new SideBarNavViewModel
            {
                MainMenu = AsyncHelper.RunSync(() => _userNavigationManager.GetMenuAsync("MainMenu", new UserIdentifier(null, (long)AbpSession.UserId))),
                ActiveMenuItemName = activeMenu
            };

            return PartialView("_SideBarNav", model);
        }
    }
}