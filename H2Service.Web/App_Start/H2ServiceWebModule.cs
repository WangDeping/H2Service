using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Localization;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Modules;
using Abp.Runtime.Caching.Redis;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Castle.MicroKernel.Registration;
using H2Service.Hangfire;
using H2Service.MeritPays;
using Microsoft.Owin.Security;
using System;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace H2Service.Web
{
    [DependsOn(                  
        typeof(H2ServiceDataModule),        
        typeof(H2ServiceApplicationModule),
        typeof(AbpWebSignalRModule),
        typeof(H2ServiceWebApiModule),
         typeof(AbpWebMvcModule),
        typeof(AbpHangfireModule),
        typeof(H2ServiceHangfireModule),
        typeof(AbpRedisCacheModule)
        )]
    public class H2ServiceWebModule : AbpModule
    {
        public override void PreInitialize()
        {           
            //配置使用Redis缓存
            Configuration.Caching.UseRedis();
            //配置所有Cache的默认过期时间为2小时    
            Configuration.Caching.ConfigureAll(cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromHours(2);
            });
           
            Configuration.Caching.Configure("WxTokenCache", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromSeconds(7100);
            }); 
            Configuration.Caching.Configure("WxJSTicketCache", cache =>
            {
                cache.DefaultSlidingExpireTime = TimeSpan.FromSeconds(7100);
            });
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-england", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));
            Configuration.Localization.Languages.Add(new LanguageInfo("zh-CN", "简体中文", "famfamfam-flag-cn"));
            Configuration.Localization.Languages.Add(new LanguageInfo("ja", "日本語", "famfamfam-flag-jp"));
           
            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new DictionaryBasedLocalizationSource(
                    H2ServiceConsts.LocalizationSourceName,
                    new XmlFileLocalizationDictionaryProvider(
                        HttpContext.Current.Server.MapPath("~/Localization/H2Service")
                        )
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<H2ServiceNavigationProvider>();
            Configuration.Modules.AbpWeb().AntiForgery.IsEnabled = false;    
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
