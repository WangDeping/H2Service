using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Authorization.Dto;
using H2Service.Dto;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
 public   interface IRoleAppService:IApplicationService
    {
        PagedResultDto<RoleDto> GetPagedRoles(GetRolesInput input);

        void CreateRole(RoleDto dto);

        void UpdateRole(RoleDto dto);

        RoleDto GetRole(int Id);

        PagedResultDto<UserDto> GetPagedUsersInRole(GetPagedUsersInRoleInput input);


        void RemoveRole(int Id);

        void GrantPermission(GrantPermissionInput input);
    }
}
