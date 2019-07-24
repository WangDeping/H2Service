using H2Service.Extensions;
using H2Service.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class HomePageValidateController : AppControllerBase
    {
        private IHomePageAppService _homePageAppService;

        public HomePageValidateController(IHomePageAppService homePageAppService) {
            _homePageAppService = homePageAppService;

        }
        //[Authorize]
        public ActionResult ValidateMessage(int Id) {            
            var msg = _homePageAppService.GetValidateMessageById(Id);         
            return View(msg);

        }
        [Authorize]
        public ActionResult GetValidateMessagesList() {

            var messages = _homePageAppService.GetValidateMessages(AbpSession.GetUserNumber());
            return View(messages);
        }

        // GET: HomePageValidate
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Default() {

            return View();

        }
    }
}