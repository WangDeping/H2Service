
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Reports;
using H2Service.Web.Models.Report;
using System.Linq;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : H2ServiceControllerBase
    {
        private IPatientsReportAppService _patientsReportAppService;
        public HomeController(IPatientsReportAppService patientsReportAppService)
        {
            _patientsReportAppService = patientsReportAppService;
        }      
       public ActionResult Index()
        {          
            return View();
         }
        [DontWrapResult]
        public JsonResult GetCurrentPatients(string dep="") {
         var list= _patientsReportAppService.GetCurrentPatientsByDep(dep);
            var model = new CurrentPatientsCounts
            {
                InPats = list.Sum(T => T.InPats),
                OutPats = list.Sum(T => T.OutPats),
                NewPats = list.Sum(T => T.NewPats),
                Deps = list.OrderByDescending(T => T.InPats).Select(T => T.Dep).ToList(),
                DepInpats = list.OrderByDescending(T => T.InPats).Select(T => T.InPats).ToList()
            };
           return Json(model, JsonRequestBehavior.AllowGet);
        }
        [DontWrapResult]
        public JsonResult GetCurrentRegistersQty(string dep) {
            var list = _patientsReportAppService.GetCurrentRegsitersQty(dep);
            var details = list.GroupBy(T => T.Hour).Select(M => new CurrentRegistersQtyDetailModel { Hour = M.Key, Qty = M.Sum(I => I.CurrentQty) }).ToList();
            var model = new CurrentRegistersQtyModel
            {
                HourList = details.Select(T => T.Hour).ToList(),
                QtyList=details.Select(T=>T.Qty).ToList(),
                Total = details.Sum(T => T.Qty)
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
         /* public ActionResult Index() {
           
            return View("~/App/Main/views/home/Index.cshtml");
        }*/
	}
}