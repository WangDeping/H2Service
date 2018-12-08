using Abp.Configuration.Startup;
using Abp.Dependency;
using Abp.Modules;
using Abp.Runtime.Caching.Redis;
using Abp.Runtime.Session;
using Abp.Web.Mvc;
using Castle.MicroKernel.Registration;
using H2Service;
using H2Service.Extensions;
using Microsoft.Owin.Security;
using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace App.App_Start
{
    [DependsOn(
       typeof(AbpWebMvcModule),
       typeof(H2ServiceDataModule),
       typeof(H2ServiceApplicationModule),
       typeof(H2ServiceWebApiModule),        
        typeof(AbpRedisCacheModule)
        )]

 public class AbpModule: Abp.Modules.AbpModule
    {
        public override void PreInitialize() {
           Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;
           Configuration.Caching.UseRedis();
            Configuration.Caching.Configure("WxTokenCache", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromSeconds(7100);
            });
            Configuration.Caching.Configure("WxJSTicketCache", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromSeconds(7100);
            });
        }
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
              Component
                  .For<IAuthenticationManager>()
                  .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                  .LifestyleTransient()
          );
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}