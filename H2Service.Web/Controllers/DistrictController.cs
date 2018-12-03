using H2Service.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class DistrictController : H2ServiceControllerBase
    {
        private readonly IDistrictAppService _districtAppService;

        public DistrictController(IDistrictAppService districtAppService)
        {
            _districtAppService = districtAppService;
        }

        public ActionResult DistrictSelect()
        {
            var list = _districtAppService.GetDistrictList().OrderByDescending(T=>T.Id);
            return PartialView("_DistrictSelect",list);
        }

        public ActionResult DistrictWithMedicalWaste()
        {
            var list = _districtAppService.GetDistrictList();
            return View(list);
           
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}