using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class SchedulingController : H2ServiceControllerBase
    {
        // GET: Scheduling
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TypeDefinitionIndex() {

            return View();
        }

        public ActionResult SetterIndex() {
            return View();
        }
    }
}