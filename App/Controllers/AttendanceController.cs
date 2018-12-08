using H2Service.WxWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    public class AttendanceController : AppControllerBase
    {
        private readonly WxTokenManager _wxTokenManager;
        public AttendanceController(WxTokenManager wxTokenManager) {
            _wxTokenManager = wxTokenManager;
        }
        // GET: Attendance
        public ActionResult Index()
        {
            string ticket = _wxTokenManager.GetWxJSApiTicket();
            this.GetWxJSApiSignature(ticket);

            return View();
        }
    }
}