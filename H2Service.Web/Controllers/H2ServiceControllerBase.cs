using Abp.Web.Mvc.Controllers;
using Castle.Core.Logging;
namespace H2Service.Web.Controllers
{
    /// <summary>
    /// Derive all Controllers from this class.
    /// </summary>
    public abstract class H2ServiceControllerBase : AbpController
    {
     
        protected H2ServiceControllerBase()
        {
            LocalizationSourceName = H2ServiceConsts.LocalizationSourceName;
         
        }
    }
}