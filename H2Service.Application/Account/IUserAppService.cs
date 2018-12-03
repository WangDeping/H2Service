using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Account.Dto;
using H2Service.Authorization;
using H2Service.Authorization.Dto;
using H2Service.Dto;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Users
{
 public   interface IUserAppService:IApplicationService
    {
       GetUserByNumberOutput GetUserByNumber(string userNumber);

        /// <summary>
        /// 根据ID获取用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        UserDto GetUserById(int Id);
        /// <summary>
        /// 根据工号和密码获取用户
        /// </summary>
        /// <param name="userNumber"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        UserDto GetUserByNumberAndPassword(string userNumber, string password);

        /// <summary>
        /// 创建账户
        /// </summary>
        /// <param name="createdto"></param>
        /// <returns></returns>
        void CreateUser(CreateUserDto createdto);


        UserDto GetUserByCode(string code);
        /// <summary>
        /// 获取分页的用户数据
        /// </summary>
        /// <param name="input">查询条件+分页请求</param>
        /// <returns></returns>
        PagedResultDto<DepartmentUserDto> GetPagedUsers(GetUsersInput input);
        List<DepartmentUserDto> GetUsersDepartmentWithDescendants(int depId);

        List<UserDto> SearchUserName(string userName);
        /// <summary>
        /// 修改用信息
        /// </summary>
        /// <param name="input"></param>
        void UpdateUser(UpdateUserInput input);
        /// <summary>
        /// 管理员重置密码
        /// </summary>
        /// <param name="Id"></param>
        void ResetPassword(int Id);
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        void RemoveUser(int Id);

        /// <summary>
        /// 获取用户下的角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<RoleDto> GetPagedRoles(GetPagedRolesInUserInput input);

        /// <summary>
        /// 给用户添加角色
        /// </summary>
        /// <param name="dto"></param>
        void AddRole(RoleUserDto dto);

        void RemoveRole(RoleUserDto dto);

        /// <summary>
        /// 分配科室
        /// </summary>
        /// <param name="dto"></param>
        void AddDepartment(DepartmentUserDto dto);


        void RemoveDepartment(DepartmentUserDto dto);

        DepartmentDto GetDefaultDepartment(int userId);

        void GrantPermission(GrantPermissionInput input);
    }
}
