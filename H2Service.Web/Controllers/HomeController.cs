
using Abp.Web.Mvc.Authorization;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : H2ServiceControllerBase
    {
     
        public HomeController()
        {
         
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