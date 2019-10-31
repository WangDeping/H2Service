using Abp.UI;
using Abp.Web.Models;
using H2Service.Reports;
using H2Service.Reports.Dto;
using H2Service.Web.Models.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class OPReportController : H2ServiceControllerBase
    {

        private readonly IOPReportAppService _OPReportAppService;

        public OPReportController(IOPReportAppService OPReportAppService) {
            _OPReportAppService = OPReportAppService;
        }
        // 门诊统计报表
        public ActionResult Index()
        {
            return View();
        }
        [DontWrapResult]
        public JsonResult GetOPQty(GetOutPatientsQtyByDepInput input) {
            if (input.Start > input.End)
                throw new UserFriendlyException("查询区间不正确");
            if ((input.End - input.Start).Days > 365) {
                throw new UserFriendlyException("查询区间不超过1年");
            }
            input.End = input.End.AddDays(1);

           var result= _OPReportAppService.GetOutPatientsQty(input);
            return Json(new { total = result.TotalCount, rows = result.Items,sum=result.SumQty }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 图表展示年度/月份就诊数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult GetOPQtyGroupPeriod(GetOutPatientsInPeriodInput input) {
            var result = _OPReportAppService.GetOutPatientsInPeriod(input);
            var model = new OPQtyInPeriodModel
            {
                PeriodList = result.Select(T => T.Date).ToList(),
                QtyList = result.Select(T => T.Qty).ToList()
            };
            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}