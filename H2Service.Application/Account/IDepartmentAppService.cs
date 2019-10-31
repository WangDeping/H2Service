using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using H2Service.Account.Dto;
using H2Service.Dto;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account
{
 public   interface  IDepartmentAppService: IApplicationService
    {    
        /// <summary>
        /// 获取所有部门树
        /// </summary>
        /// <returns></returns>
        IEnumerable<DepartmentTreeOutput> GetDepartmentTree(string parent="#");

        DepartmentDto GetRootDepartment();

        void CreateDepartment(DepartmentDto dto);

        void UpdateDepartment(DepartmentDto dto);

        DepartmentDto GetById(int Id);

        /// <summary>
        /// 改变上级部门
        /// </summary>
        /// <param name="dto"></param>
        void ChangeFather(DepartmentDto dto);

        List<DepartmentUserDto> GetUserInDepartments(long Id);

        void AssginDepartment(DepartmentUserDto dto);

        void RemoveDepartmentUser(DepartmentUserDto dto);

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="Id"></param>
        void RemoveDepartment(int Id);
        /// <summary>
        /// 获取该部门及所有祖先部门
        /// </summary>
        /// <param name="Id">部门Id</param>
        /// <returns></returns>
        IEnumerable<DepartmentDto> DepartmentWithAncestors(int Id);
        /// <summary>
        /// 获取该部门及所有子孙部门
        /// </summary>
        /// <param name="Id">部门Id</param>
        /// <returns></returns>
        IEnumerable<DepartmentDto> DepartmentWithDescendants(int Id);


        //DepartmentTreeSelect2Output GetDepartmentsSelect2(int Id);
        /// <summary>
        /// 将权限分给部门及子孙部门
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="permissions"></param>
        void GrantPermission(GrantPermissionInput input);

        /// <summary>
        /// 把部门关联到业务模块
        /// </summary>
        /// <param name="dto"></param>
        void CreateDepartmentModuleRelation(DepartmentModulesRelationDto dto);
        List<DepartmentModulesRelationDto> GetRelatedDepartments(int module);
        void RemoveRelation(int Id);

        List<UserDto> GetUserByDepartmentId(int Id);
    }
}
