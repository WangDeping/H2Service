using H2Service.MeritPays;
using H2Service.WxWork;
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
        private readonly WxTokenManager _wxTokenManager;
        public MeritPayController(IMeritPayAppService meritPayAppService,
           WxTokenManager wxTokenManager)
        {
            _meritPayAppService = meritPayAppService;
            _wxTokenManager = wxTokenManager;

        }


        [Authorize]
        public ActionResult PersonalMeritPay(int Id)
        {
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);
            var output = _meritPayAppService.PersonalDetails(Id);
            return View(output);
        }
    }
}