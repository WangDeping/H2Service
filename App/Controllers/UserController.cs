using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Account;
using H2Service.Account.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [AbpMvcAuthorize]
    public class UserController : AppControllerBase
    {
        private readonly DepartmentAppService _departmentAppService;
        public UserController(DepartmentAppService departmentAppService)
        {
            _departmentAppService = departmentAppService;

        }
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult  SetDepartment()
        {
            var departments = _departmentAppService.GetDepartmentAllowedUser();            
            return View(departments);

        }
        /// <summary>
        /// 用户自己设置科室
        /// </summary>
        /// <param name="departmentId"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult PutUserDepartment(int departmentId)
        {
            var userId = AbpSession.GetUserId();
            var assignedDeps = _departmentAppService.GetUserInDepartments(userId);
            if (assignedDeps != null && assignedDeps.Count > 0)
            {
                var defaultDep = assignedDeps.First().DepartmentName;
                return Json(new ErrorInfo(-1, "已在"+defaultDep));
             }
            _departmentAppService.AssginDepartment(new DepartmentUserDto {
                  DepartmentId=departmentId, UserId=userId
            });
            return Json(new ErrorInfo(0, "保存成功"));
        }
    }
}