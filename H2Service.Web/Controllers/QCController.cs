using Abp.UI;
using Abp.Web.Models;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.QC;
using H2Service.QC.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class QCController : H2ServiceControllerBase
    {
        private IQCAppService _qCAppService;
        public QCController(IQCAppService qCAppService)
        {
            _qCAppService = qCAppService;

        }
        // GET: QC
        public ActionResult Index()
        {
            _qCAppService.Test();
            return View();
        }
        public ActionResult CreatePeriod()
        {
            return View();

        }
        [DontWrapResult]
        public JsonResult PeriodsGrid(PagedInputDto request)
        {
            var result = _qCAppService.GetPagedQCAppraisalPeriod(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);

        }
        [DontWrapResult]
        public JsonResult GetPeriod(int Id)
        {
            var result = _qCAppService.GetPeriod(Id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddorUpdatePeriod(QCAppraisalPeriodDto request)
        {
            if (request.Id == 0)
                _qCAppService.CreatePeriod(request);
            else
                _qCAppService.UpdatePeriod(request);
            return Json(new ErrorInfo(0, "保存成功"));
        }

      
        public ActionResult FDIndex() {           
            return View();

        }
        /// <summary>
        /// 填写督察扣分项
        /// </summary>
        /// <param name="Id">PeriodId</param>
        /// <returns></returns>
        public ActionResult FillIndex(int Id) {
            if (string.IsNullOrEmpty(AbpSession.GetDepartmentId()) || AbpSession.GetDepartmentId() == "0")
                throw new UserFriendlyException("您在H2Service系统中还没有分配科室！");
            var period = _qCAppService.GetPeriod(Id);
            ViewBag.Period = period;
            return View();
        }
        /// <summary>
        /// 职能科室扣分表格
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult FuncDepDetailsGrid(GetPagedFunctionalDepartmentDetailsInput request)
        {
            request.FunctionalDepartmentId = int.Parse(AbpSession.GetDepartmentId());
            var result = _qCAppService.GetDetailsByPeriod(request.DepartmentPunishmentPeriodId)
                .Where(T=>T.FunctionalDepartmentId== request.FunctionalDepartmentId)
                .OrderByDescending(T => T.PunishedDepartmentId);
            var count = result.Count();      
            return Json(new { total = count, rows = result }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult AddorUpdateDetail(QCDetailDto request) {
            if (request.Id == 0)
                _qCAppService.CreateDetail(request);
            return Json(new ErrorInfo(0, "保存成功"));
        }

        public JsonResult RemoveDetail(int Id)
        {
            _qCAppService.RemoveDetail(Id);
            return Json(new ErrorInfo(0, "删除成功"));
        }
    }
}