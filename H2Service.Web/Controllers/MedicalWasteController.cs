using Abp.Application.Services.Dto;
using Abp.UI;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.MedicalWastes;
using H2Service.MedicalWastes.Dto;
using H2Service.Web.Helpers;
using H2Service.Web.Models.MedicalWaste;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Configuration;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class MedicalWasteController : H2ServiceControllerBase
    {
        private readonly IMedicalWasteAppService _medicalWasteAppService;
        public MedicalWasteController(IMedicalWasteAppService medicalWasteAppService)
        {
            _medicalWasteAppService = medicalWasteAppService;
        }
        public ActionResult Index()
        {
            ViewBag.QrcodeBaseUrl = ConfigurationManager.AppSettings["appBaseUrl"] + "MedicalWaste/Index/";
            ViewBag.StartDate = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd");
            return View();
        }
        [DontWrapResult]
        [AbpMvcAuthorize(PermissionNames.Pages_Infection_MedicalWasteManage)]
        public JsonResult MedicalWasteGrid(MedicalWasteGridRequestModel request)
        {
            var result = _medicalWasteAppService.GetHandOverFlowList(
                new GetHandOverFlowListInput {
                    DepartmentId =request.DepartmentId,
                    End =request.End,
                    Start =request.Start}
                );
            var  items=result.Skip(request.SkipCount).Take(request.MaxResultCount).ToList();
            return Json(new { total = result.Count(), rows =items }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 导出查询的医疗废物交接表
        /// </summary>
        /// <param name="request"></param>
        /// <returns>下载路径</returns>
     
        [AbpMvcAuthorize(PermissionNames.Pages_Infection_MedicalWasteManage)]
        public JsonResult ExportMedicalWasteGrid(MedicalWasteGridRequestModel request)
        {
            var result = _medicalWasteAppService.GetHandOverFlowList(
                new GetHandOverFlowListInput
                {
                    DepartmentId = request.DepartmentId,
                    End = request.End,
                    Start = request.Start
                }
                );
            var shortName = @"/tmpFiles/" + DateTime.Now.ToFileTime().ToString() + ".xlsx";
            var fileName= Server.MapPath(@"~/"+shortName);
            var headerArrary=new string[] { "感染性", "损伤性", "病理性", "化学性", "药学性",};
            ExcelHelper.ExportEasy(result,fileName,headerArrary);
            return  Json(shortName);
        }

        public ActionResult Detail(int Id) {
            var currentUrl = Request.Url.ToString();
            var wasteImg = "Content/images/medicalwaste.png";
            if (currentUrl.Contains(WebConfigurationManager.AppSettings["extranet"]))
                wasteImg = WebConfigurationManager.AppSettings["extranet"] + wasteImg;
            else
                wasteImg = WebConfigurationManager.AppSettings["intranet"] + wasteImg;
            ViewBag.WasteFlgImg = wasteImg;
            var wasteDisplay = _medicalWasteAppService.GetMedicalWasteByFlowId(Id);
            return View(wasteDisplay);
        }

        public ActionResult QrcodeIndex() {

            return View();
        }

        public PartialViewResult FlowStatusSelect()
        {

            return PartialView("_FlowStatusSelect");

        }
        public ActionResult StatisticsIndex()
        {
            ViewBag.StartDate = DateTime.Now.AddDays(-6).ToString("yyyy-MM-dd");
            return View();

        }
        [DontWrapResult]
        public JsonResult BargraphStatistics(WasteStatisticInput request)
        {
            request.End = request.End.AddDays(1);
            var result = _medicalWasteAppService.WasteStatistic(request);
            var echartsbar = new EchartsBaseDto();
            var serie_weight = new EchartsBaseSerieDto { name = "重量(Kg)", type = "bar" };
            var serie_count = new EchartsBaseSerieDto { name = "数量(包/盒)", type = "bar" };
            foreach (var r in result)
            {
                echartsbar.XAxis.data.Add(r.Kind.ToString());
                serie_weight.data.Add(r.Total);
                serie_count.data.Add(r.PackageCount);
            }
            echartsbar.Series.Add(serie_weight);
            echartsbar.Series.Add(serie_count);
            return Json(echartsbar, JsonRequestBehavior.AllowGet);
        }

        [DontWrapResult]
        public JsonResult LinegraphStatisticsIn7Days(WasteStatisticInput request)
        {
            var today = DateTime.Now.Date.AddDays(1);//今天的24点
            request.Start = today.AddDays(-8);//七天前
            var echartsline = new EchartsBaseDto();
            var serie_kind_weight_list = new List<EchartsBaseSerieDto>();
            foreach (MedicalWasteKind kind in Enum.GetValues(typeof(MedicalWasteKind)))
            {
                serie_kind_weight_list.Add(new EchartsBaseSerieDto { name = kind.ToString(), type = "line" });
            }
            for (int i = 0; i <= 6; i++)
            {
                request.Start = request.Start.AddDays(1);
                request.End = request.Start.AddDays(1).AddSeconds(-1);
                var result = _medicalWasteAppService.WasteStatistic(request);
                echartsline.XAxis.data.Add(request.Start.ToShortDateString());//横轴，近七天日期
                foreach (var r in result)
                    serie_kind_weight_list.FirstOrDefault(T => T.name == r.Kind.ToString()).data.Add(r.Total);
            }

            echartsline.Series = serie_kind_weight_list;
            return Json(echartsline, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeliveryHistroyIndex() {                       
            return View();
        }       

        [DontWrapResult]
        public JsonResult GetDeliveryHistoryGrid(GetPagedDeliveryInput request)
        {
            request.DistrictId = 0;
            var result = _medicalWasteAppService.GetPagedDeliveryHistory(request);          
            return Json(new { total =result.TotalCount, rows =result.Items }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult Warning()
        {
            return View();

        }

        public ActionResult GetDontHaveWasteDepartments(int days)
        {
            var list = _medicalWasteAppService.GetDepartmentsWhoDontHaveWaste(days);
            return PartialView("_DontHaveWasteDepartments",list);
        }

        public ActionResult GetDontHandOverWasteDepartments(int days)
        {
            var list = _medicalWasteAppService.GetDepartmentsWhoDontHandoverWaste(days);
            return PartialView("_DontHaveWasteDepartments", list);
        }


        public ActionResult TraceIndex() {
            return View();
        }

        public ActionResult GetTraceInfo(string code) {
            var traceInfo = _medicalWasteAppService.GetTraceInfo(code);
            return PartialView("_GetTraceInfo",traceInfo);

        }
    }
}