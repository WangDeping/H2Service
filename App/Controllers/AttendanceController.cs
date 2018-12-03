using H2Service.WeChatWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AttendanceController : AppControllerBase
    {
        private readonly IWxAppService _wxAppService;
        public AttendanceController(IWxAppService wxAppService) {
            _wxAppService = wxAppService;
        }
        // GET: Attendance
        public ActionResult Index()
        {            
            string ticket = _wxAppService.GetJSApiTicket();
            this.GetWxJSApiSignature(ticket);

            return View();
        }
    }
}