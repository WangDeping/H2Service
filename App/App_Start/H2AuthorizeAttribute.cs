using Abp.Runtime.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace App.App_Start
{
    public class H2AuthorizeAttribute: AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
          /*  var userId= Thread.CurrentPrincipal?.Identity?.Name;
            if (!string.IsNullOrEmpty(userId)) {
                //创建identity
                var identity = new GenericIdentity(userId);
                // 添加Type为AbpClaimTypes.UserId使userId能注入到AbpSession
                identity.AddClaim(new Claim(AbpClaimTypes.UserId,userId));
                // 创建principal
                var principal = new GenericPrincipal(identity,null);
                // 同步Thread.CurrentPrincipal
                Thread.CurrentPrincipal = principal;
                if (HttpContext.Current != null)
                {
                    // 将principal赋值给HttpContext.Current.User,用户登入。
                    HttpContext.Current.User = principal;
                }
            }*/
            base.OnAuthorization(filterContext);
        }
    }
}