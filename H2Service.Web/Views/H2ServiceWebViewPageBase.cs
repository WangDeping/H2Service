using Abp.Web.Mvc.Views;

namespace H2Service.Web.Views
{
    public abstract class H2ServiceWebViewPageBase : H2ServiceWebViewPageBase<dynamic>
    {

    }

    public abstract class H2ServiceWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected H2ServiceWebViewPageBase()
        {
            LocalizationSourceName = H2ServiceConsts.LocalizationSourceName;
        }
    }
}