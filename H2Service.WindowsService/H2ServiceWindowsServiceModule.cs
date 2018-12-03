using Abp.Modules;
using H2Service.WxWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WindowsService
{
    [DependsOn(typeof(WxWrokModule))]
    public   class H2ServiceWindowsServiceModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
