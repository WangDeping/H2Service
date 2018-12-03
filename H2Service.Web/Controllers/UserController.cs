using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Web.Models;
using Abp.Web.Mvc.Authorization;
using H2Service.Account;
using H2Service.Account.Dto;
using H2Service.Authorization.Dto;
using H2Service.Dto;
using H2Service.Users;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace H2Service.Web.Controllers
{
    //[AbpMvcAuthorize("Pages.Users")]
    public class UserController: H2ServiceControllerBase
    {
        private IUserAppService _userAppService;
        private IDepartmentAppService _departmentAppService;
        public UserController(IUserAppService userAppService,
            IDepartmentAppService departmentAppService)
        {
            _userAppService = userAppService;
            _departmentAppService = departmentAppService;

        }

        public ActionResult Index()
        {
            return View();

        }
        [DontWrapResult]
        public JsonResult UserGrid(GetUsersInput request)
        {
           var result=_userAppService.GetPagedUsers(request);
          
            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }
        [DontWrapResult]
        public JsonResult UserGridInDepartment(GetUsersInput request)
        {
            var query = _userAppService.GetUsersDepartmentWithDescendants(request.DepartmentId);
            var queryCount = query.Count();
            var users = query.OrderByDescending(T => T.Id).Skip(request.SkipCount).Take(request.MaxResultCount).ToList();
            var result= new PagedResultDto<DepartmentUserDto> { Items = users, TotalCount = queryCount };
            return Json(new { total =queryCount, rows =users }, JsonRequestBehavior.AllowGet);
        }

        [DontWrapResult]
        public JsonResult Select2UsersInDepartmet(Select2UsersInDepartmetInput request) {
            
            if (!string.IsNullOrEmpty(request.userName))
            {
               var  result = _userAppService.SearchUserName(request.userName).Select(T=>new {text=T.UserName+"-"+T.UserNumber,id=T.Id });
                return Json(new {  items=result.Skip(request.page*request.rows).Take(request.rows), total_count =result.Count()}, JsonRequestBehavior.AllowGet);
            }
          
            else
            {
              
               var  result = _userAppService.GetUsersDepartmentWithDescendants(request.departmentId).Select(T=> new { text = T.UserName + "-" + T.UserNumber, id = T.Id });
                return Json(new { items = result.Skip(request.page * request.rows).Take(request.rows), total_count = result.Count() }, JsonRequestBehavior.AllowGet);
            }
        }

        [DontWrapResult]
        public JsonResult GetUser(int Id)
        {
            var user = _userAppService.GetUserById(Id);
            return Json(user, JsonRequestBehavior.AllowGet);
        }
       
        public JsonResult UpdateUser(UpdateUserInput input) {

            _userAppService.UpdateUser(input);
            return Json(new ErrorInfo(0, "保存成功"));
        }
        public JsonResult ResetPassword(int Id) {
            _userAppService.ResetPassword(Id);
            return Json(new ErrorInfo(0, "保存成功"));

        }
        public JsonResult RemoveUser(int Id) {
            _userAppService.RemoveUser(Id);
            return Json(new ErrorInfo(0, "保存成功"));
        }
        [DontWrapResult]
        public JsonResult GetRoles(GetPagedRolesInUserInput request)
        {
            var result = _userAppService.GetPagedRoles(request);

            return Json(new { total = result.TotalCount, rows = result.Items }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AddRole(RoleUserDto request)
        {
            _userAppService.AddRole(request);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" },JsonRequestBehavior.AllowGet);
        }
        public JsonResult RemoveUserRole(RoleUserDto request)
        {
            _userAppService.RemoveRole(request);
            return Json(new ErrorInfo { Code = 0, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取部门树
        /// </summary>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult DepartmentTree() {

            var result = _departmentAppService.GetDepartmentTree().ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// 获取用户所有的所属部门
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [DontWrapResult]
        public JsonResult DepartmentUsersGrid(int Id)
        {
            var result = _departmentAppService.GetUserInDepartments(Id);           
            return Json(result, JsonRequestBehavior.AllowGet);
        }    

        public JsonResult AssginDepartment(DepartmentUserDto dto) {
            _userAppService.AddDepartment(dto);
            return Json(new ErrorInfo { Code = 0, Message = "保存成功" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveDepartment(DepartmentUserDto dto) {
            _userAppService.RemoveDepartment(dto);
            return Json(new ErrorInfo { Code = 0, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GrantPermission(GrantPermissionInput input)
        {
            _userAppService.GrantPermission(input);
            return Json(new ErrorInfo { Code = 0, Message = "删除成功" }, JsonRequestBehavior.AllowGet);
        }
    }
}