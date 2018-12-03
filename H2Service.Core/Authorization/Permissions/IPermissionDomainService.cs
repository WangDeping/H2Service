using Abp.Authorization;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Permissions
{
 public  interface IPermissionDomainService: IDomainService
    {
        List<Permission> GetAllPermissions();
        IEnumerable<UserPermission> GetPermissionByUser(long userId);
        IEnumerable<RolePermission> GetPermissionByRole(int roleId);
        IEnumerable<DepartmentPermission> GetPermissionByDepartment(int depId);
        Permission GetPermission(string name = null);
        void AssignPermissionToUser(long userId, List<Permission> permissions);
        void AssignPermissionToDepartment(int depId, List<Permission> permissions);
        void AssignPermissionToRole(int roleId, List<Permission> permissions);
    }
}
