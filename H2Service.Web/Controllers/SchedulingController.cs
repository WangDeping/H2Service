using Abp.Web.Models;
using Castle.Core.Logging;
using H2Service.Authorization.Departments;
using H2Service.Scheduling;
using H2Service.Scheduling.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    [Abp.Authorization.AbpAuthorize]
    public class SchedulingController : H2ServiceControllerBase
    {
        private readonly ISchedulingAppService _schedulingAppService;      
        public SchedulingController(ISchedulingAppService schedulingAppService) {

            _schedulingAppService = schedulingAppService;
           
        }
        // GET: Scheduling
        public ActionResult Index()
        {
            var result = _schedulingAppService.GetSchedulingTypeGroups();           
            return View(result);
        }

        public ActionResult TypeDefinitionIndex() {

            return View();
        }
        /// <summary>
        /// 排班对象维护
        /// </summary>
        /// <returns></returns>
        public ActionResult SetterIndex() {
            var departments = _schedulingAppService.GetDepartmentsBySchedulingUserId(AbpSession.UserId.Value);
            return View(departments);
        }
        [DontWrapResult]
        public JsonResult DepartmentUsersGrid(int departmentId) {
            var result = _schedulingAppService.GetSchedulingDepartmentUsers(departmentId);
            return Json(new { total = result.Count(), rows = result }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateSchedulingDepartmentUser(CreateDepartmentUserInput request) {
            _schedulingAppService.CreateDepartmentUser(request);
            return Json(new ErrorInfo {  Code=1, Message="保存成功"});
        }
        public JsonResult RemoveSchedulingDepartmentUser(RemoveDepartmentUserInput request)
        {
            _schedulingAppService.RemoveDepartmentUser(request);
            return Json(new ErrorInfo { Code = 1, Message = "操作成功" });
        }
        [DontWrapResult]
        public JsonResult GetTypeGroupsGrid() {

            var result = _schedulingAppService.GetSchedulingTypeGroups();
            return Json(new { total = result.Count(), rows = result }, JsonRequestBehavior.AllowGet);
        }       
        public JsonResult PutTypeGroup(SchedulingTypeGroupDto request) {
            if(request.Id<=0)
                _schedulingAppService.CreateTypeGroup(request.SchedulingGroupName);
            else
                _schedulingAppService.UpdateTypeGroup(request);
            return Json(new ErrorInfo {  Code=1, Message="保存成功"}, JsonRequestBehavior.AllowGet);
        }
        [DontWrapResult]
        public JsonResult GetTypesGrid(int groupId) {
            var result = _schedulingAppService.GetSchedulingTypes(groupId);
            return Json(new { total = result.Count(), rows = result }, JsonRequestBehavior.AllowGet);
        }
        [DontWrapResult]
        public JsonResult GetSchedulingType(int typeId) {
            var result = _schedulingAppService.GetSchedulingType(typeId);
            return Json(result);
        }
        public JsonResult PutSchedulingType(SchedulingTypeDto request) {
            if (request.Id == 0)
                _schedulingAppService.CreateType(request);
            else
                _schedulingAppService.UpdateType(request);
            return Json(new ErrorInfo { Code = 1, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 给人员设置排班权限
        /// </summary>
        /// <returns></returns>
        public ViewResult SchedulingPermissionIndex() {
            // return View();
            ViewBag.Module = (int)H2Module.需排班科室;
            return View(@"~/Views/H2Module/Index.cshtml");
        }
  
    }
}