using System.Reflection;
using Abp.Modules;
using H2Service.Authorization;
using Abp.Dependency;
using Abp.Runtime.Session;
using H2Service.Extensions;
using System.ComponentModel;
using Abp.Authorization;

namespace H2Service
{

    public class H2ServiceCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
           
        }
        public override void PreInitialize()
        {
            
            Configuration.Authorization.Providers.Add<H2ServiceAuthorizationProvider>();
            //Configuration.Settings.Providers.Add<AppSettingProvider>();

            
            Configuration.MultiTenancy.IsEnabled = false;            
            
        }
    }
}
