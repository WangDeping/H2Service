using H2Service.MeritPays;
using H2Service.WeChatWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class MeritPayController : AppControllerBase
    {
        private IMeritPayAppService _meritPayAppService;
        private readonly IWxAppService _wxAppService;
        public MeritPayController(IMeritPayAppService meritPayAppService,
            IWxAppService wxAppService)
        {
            _meritPayAppService = meritPayAppService;
            _wxAppService = wxAppService;

        }


        [Authorize]
        public ActionResult PersonalMeritPay(int Id)
        {
            string ticket = _wxAppService.GetJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            var output = _meritPayAppService.PersonalDetails(Id);
            return View(output);
        }
    }
}