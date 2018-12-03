using Abp.Modules;
using Abp.Runtime.Validation.Interception;
using Castle.Core;
using Castle.MicroKernel;
using H2Service.Hangfire.Framework;
using System.Reflection;


namespace H2Service.Hangfire
{
    //[DependsOn(typeof(H2ServiceApplicationModule))]
    public   class H2ServiceHangfireModule: AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //Ensure our input classes are validated when used
            IocManager.IocContainer.Kernel.ComponentRegistered += Kernel_ComponentRegistered;
        }

        private void Kernel_ComponentRegistered(string aKey, IHandler aHandler)
        {
            if (typeof(IHangfireParamsInputBase).GetTypeInfo().IsAssignableFrom(aHandler.ComponentModel.Implementation))
                //Add validation to the inputs for jobs
                aHandler.ComponentModel.Interceptors.Add(new InterceptorReference(typeof(ValidationInterceptor)));
        }
    }
}

  
