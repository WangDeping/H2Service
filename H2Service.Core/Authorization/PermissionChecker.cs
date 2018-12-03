using Abp.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp;
using Abp.Dependency;
using Abp.Runtime.Session;
using Castle.Core.Logging;
using Castle.Windsor;

namespace H2Service.Authorization
{
    public class PermissionChecker : IPermissionChecker, ITransientDependency
    {
        public IAbpSession AbpSession { get; set; }
        private readonly UserManager _userManager;
        public PermissionChecker(UserManager userManager)
        {
            _userManager = userManager;  
        }
        public virtual  Task<bool> IsGrantedAsync(string permissionName)
        {
            var userId = AbpSession.GetUserId();
            return IsGrantedAsync(new UserIdentifier(null, userId), permissionName);


        }

        public virtual  Task<bool> IsGrantedAsync(UserIdentifier user, string permissionName)
        {
            var t = new Task<bool>(() =>
            {
                //var container = new WindsorContainer();
                //_userManager = container.Resolve<IUserManager>();
                return _userManager.IsGranted(user.UserId, permissionName);      
                //return false;
            });
            t.Start();
            return t;
        }
       
    }
}
