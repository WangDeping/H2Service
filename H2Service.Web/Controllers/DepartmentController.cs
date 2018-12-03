using Abp.Web.Models;
using Abp.Web.Mvc.Controllers;
using H2Service.Account;
using H2Service.Account.Dto;
using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class DepartmentController : H2ServiceControllerBase
    {
        private IDepartmentAppService _departmentAppService;
        public DepartmentController(IDepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;
        }
        public ActionResult Index()
        {
            return View();
        }
        [DontWrapResult]
        public JsonResult GetDepartmentById(int Id)
        {
            var department = _departmentAppService.GetById(Id);
            return Json(department, JsonRequestBehavior.AllowGet);
        }
        [DontWrapResult]
        public JsonResult DepartmentTree(string parent="#")
        {          
            var result = _departmentAppService.GetDepartmentTree(parent).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult DepartmentsSelect2PartialViewResult()
        {
            var rootId = _departmentAppService.GetRootDepartment().Id;
            var result = _departmentAppService.DepartmentWithDescendants(rootId);
            return PartialView("_DepartmentsSelect2PartialViewResult",result);
        }
      
        public JsonResult SetPermission(GrantPermissionInput input)
        {
            _departmentAppService.GrantPermission(input);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult AddorUpdateDepartment(DepartmentDto dto)
        {
            if (dto.Id == 0)
                _departmentAppService.CreateDepartment(dto);
            else
                _departmentAppService.UpdateDepartment(dto);
            return Json(new ErrorInfo(0, "保存成功"));
        }
        public JsonResult ChangeFather(DepartmentDto dto)
        {
            _departmentAppService.ChangeFather(dto);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveDepartment(int Id)
        {
            _departmentAppService.RemoveDepartment(Id);
            return Json(new ErrorInfo { Code = 0, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AssginDepartment(DepartmentUserDto dto)
        {
            _departmentAppService.AssginDepartment(dto);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveDepartmentUser(DepartmentUserDto dto)
        {
            _departmentAppService.RemoveDepartmentUser(dto);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RelateModuleIndex()
        {
            return View();
        }

        public JsonResult RelateModule(DepartmentModulesRelationDto dto)
        {
            _departmentAppService.CreateDepartmentModuleRelation(dto);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult RelatedDepartments(int module=-1) {
            var relations = _departmentAppService.GetRelatedDepartments(module);
            return PartialView("_RelatedModulesList", relations);

        }
        public ActionResult RelatedDepartmentsSelect(int module = -1)
        {
            var relations = _departmentAppService.GetRelatedDepartments(module);
            return PartialView("_RelatedDepartmentsSelectPartialViewResult", relations);

        }
        public JsonResult RemoveRelation(int Id)
        {
            _departmentAppService.RemoveRelation(Id);
            return Json(new ErrorInfo { Code = 0, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}