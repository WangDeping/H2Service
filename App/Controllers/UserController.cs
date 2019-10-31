using Abp.Runtime.Session;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Account;
using H2Service.Account.Dto;
using H2Service.Users;
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
        private readonly IUserAppService _userAppService;
        public UserController(DepartmentAppService departmentAppService, IUserAppService userAppService)
        {
            _departmentAppService = departmentAppService;
            _userAppService = userAppService;

        }
        // GET: User
        public ActionResult Index()
        {
            var user = _userAppService.GetUserById((int)AbpSession.GetUserId());
            return View(user);
        }
        public ActionResult  SetDepartment(int moduleId=-1)
        {
            var departments = new List<DepartmentDto>();
            if (moduleId < 0)
            {
                departments = _departmentAppService.GetDepartmentAllowedUser().ToList();
                return View(departments);
            }
            else {
                departments = _departmentAppService.GetRelatedDepartments(moduleId).Select(S=>
                new DepartmentDto {  DepartmentName=S.DepartmentName, Id=S.DepartmentId}).ToList();
                return View(departments);
            }

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
                _departmentAppService.RemoveDepartmentUser(new DepartmentUserDto { UserId = userId, DepartmentId = assignedDeps.First().DepartmentId });               
             }
            _departmentAppService.AssginDepartment(new DepartmentUserDto {
                  DepartmentId=departmentId, UserId=userId
            });
            return Json(new ErrorInfo(0, "保存成功"));
        }
    }
}