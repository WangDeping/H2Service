using Abp.Web;
using App.App_Start;
using Castle.Facilities.Logging;
using System;
using Abp.Castle.Logging.Log4Net;
namespace App
{
    public class MvcApplication : AbpWebApplication<AbpModule>
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            AbpBootstrapper.IocManager.IocContainer.AddFacility<LoggingFacility>(
                             f => f.UseAbpLog4Net().WithConfig(Server.MapPath("log4net.config"))
                         );
            base.Application_Start(sender, e);
        }
    }
}
