
using System.Web.Mvc;

using System.Collections.Generic;
using Hangfire;
using H2Service.Hangfire.Jobs.DailyInPatients;
using H2Service.Hangfire.Jobs.DailyInPatients.Dto;
using H2Service.WeChatWork;
using Abp.Web.Mvc.Authorization;
using H2Service.Authorization;
using Abp.Authorization;

namespace H2Service.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : H2ServiceControllerBase
    {
        private IWxAppService _wxAppService{ get; set; }
        public HomeController(IWxAppService wxAppService)
        {
            _wxAppService = wxAppService;
        }      
       public ActionResult Index()
        {
          
            return View();
         }
         /* public ActionResult Index() {
           
            return View("~/App/Main/views/home/Index.cshtml");
        }*/
	}
}