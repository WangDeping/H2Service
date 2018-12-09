using Abp.Web.Models;
using H2Service.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class QrCodeController : H2ServiceControllerBase
    {
        // GET: qrCode
        public ActionResult Index()
        {
            return View();
        }

        [DontWrapResult]
        public ActionResult GetQrCode(string content)
        {         
            var stream = QrCodeHelper.CommonQrCode(content);
            return File(stream.ToArray(), "image/jpeg");

        }
    }
}