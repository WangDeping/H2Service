using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using System.Linq;
using Swashbuckle.Application;
using H2Service.MedicalWastes;

namespace H2Service
{
    [DependsOn(typeof(AbpWebApiModule), typeof(H2ServiceApplicationModule))]
    public class H2ServiceWebApiModule : AbpModule
    {
      
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder.For<IMedicalWasteAppService>("system/medicalwaste").Build();
            //Configuration.Modules.AbpWebApi().DynamicApiControllerBuilder
            //    .ForAll<IApplicationService>(typeof(H2ServiceApplicationModule).Assembly, "app")
            //    .Build();


        }

        private void ConfigureSwaggerUi()
        {
            Configuration.Modules.AbpWebApi().HttpConfiguration
                .EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "SwaggerIntegrationDemo.WebApi");
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                })
                .EnableSwaggerUi(c =>
                {
                    c.InjectJavaScript(Assembly.GetAssembly(typeof(H2ServiceWebApiModule)), "H2ServiceWebApi.Scripts.Swagger-Custom.js");
                });
        }
    }
}
