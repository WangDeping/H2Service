using Abp.UI;
using Abp.Web.Models;
using H2Service.Authorization.Departments;
using H2Service.H2Modules;
using H2Service.H2Modules.Dto;
using H2Service.Web.Models.H2Module;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace H2Service.Web.Controllers
{
    public class H2ModuleController : H2ServiceControllerBase
    {

        private readonly H2ModuleAppService _h2ModuleAppService;
        public H2ModuleController(H2ModuleAppService h2ModuleAppService)
        {

            _h2ModuleAppService = h2ModuleAppService;

        }
        public ActionResult Index()
        {
            return View();
        }

        [DontWrapResult]
        public JsonResult GetPagedModuleDepartments(GetPagedH2ModuleWithAuditingInput request)
        {

            var result = _h2ModuleAppService.GetPagedH2ModuleWithAuditing(request);
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult SetModulePermission(SetModulePermissionModel request)
        {            
            var input = new H2ModuleWithAuditingDto
            {
                AuditorId = request.AuditorId,
                DepartmentId = request.DepartmentId,
                DoUserId = request.DoUserId,
                Id = request.Id,
                Module = (H2Module)request.Module
            };
           
            if (input.DoUserId > 0)
                _h2ModuleAppService.SetModuleDoUserWithPermission(input, request.PermissionName);
            else if (input.AuditorId > 0)
                _h2ModuleAppService.SetModuleAuditorWithPermission(input, request.PermissionName);
            else
                throw new UserFriendlyException("没有选中要设置权限的用户");
            return Json(new ErrorInfo { Code = 1, Message = "保存成功" }, JsonRequestBehavior.AllowGet);

        }
    }
}