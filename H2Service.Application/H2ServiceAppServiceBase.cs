using Abp.Application.Services;
using H2Service.Extensions;

namespace H2Service
{
    /// <summary>
    /// Derive your application services from this class.
    /// </summary>
    public abstract class H2ServiceAppServiceBase : ApplicationService
    {
        public new IAbpSessionExtension AbpSession { get; set; }
        protected H2ServiceAppServiceBase()
        {
            LocalizationSourceName = H2ServiceConsts.LocalizationSourceName;
        }
    }
}