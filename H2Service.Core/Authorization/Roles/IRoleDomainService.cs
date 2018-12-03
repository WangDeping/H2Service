using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Roles
{
 public  interface IRoleDomainService: IDomainService
    {
        void RemoveRoleWithUsersAndPermission(int roleId);

        bool IsGranted(int roleId, string permissionName);
    }
}
