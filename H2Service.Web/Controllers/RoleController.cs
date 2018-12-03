using Abp.Web.Models;
using Abp.Web.Mvc.Controllers;
using H2Service.Authorization;
using H2Service.Authorization.Dto;
using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class RoleController : H2ServiceControllerBase
    {
        private readonly IRoleAppService _roleAppService;

        public RoleController(IRoleAppService roleAppService)
        {
            _roleAppService = roleAppService;

        }

        public ActionResult Index()
        {
            return View();
        }

        [DontWrapResult]
        public JsonResult RoleGrid(GetRolesInput request)
        {
            var result = _roleAppService.GetPagedRoles(request);

            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddorUpdate(RoleDto dto)
        {
            if (dto.Id == 0)
                _roleAppService.CreateRole(dto);
            else
                _roleAppService.UpdateRole(dto);
            return Json(new ErrorInfo(0, "保存成功"));

        }
        [DontWrapResult]
        public JsonResult Get(int Id)
        {
            return Json(_roleAppService.GetRole(Id), JsonRequestBehavior.AllowGet);
        }

        [DontWrapResult]
        public JsonResult GetUsers(GetPagedUsersInRoleInput request)
        {
            var result = _roleAppService.GetPagedUsersInRole(request);

            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(int Id)
        {
            _roleAppService.RemoveRole(Id);
            return Json(new ErrorInfo(0, "保存成功"));
        }

        public JsonResult GrantPermission(GrantPermissionInput input)
        {
            _roleAppService.GrantPermission(input);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}