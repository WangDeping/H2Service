using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using H2Service.EntityFramework;

namespace H2Service
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(H2ServiceCoreModule))]
    public class H2ServiceDataModule : AbpModule
    {
        public override void PreInitialize()
        {           
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<H2ServiceDbContext>(null);
        }
    }
}
