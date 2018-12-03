using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Roles
{
 public   class RoleDomainService: DomainService, IRoleDomainService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        public RoleDomainService(IRepository<Role> roleRepository,
            IRepository<RolePermission> rolePermissionRepository)
        {
            _roleRepository = roleRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }

        public void RemoveRoleWithUsersAndPermission(int roleId)
        {
            var users =_roleRepository.Get(roleId).Users.ToList();
            for (int i = users.Count - 1; i >= 0; i--)
            {
                users.RemoveAt(i);
            }
            _rolePermissionRepository.Delete(T => T.RoleId == roleId);
            _roleRepository.Delete(roleId);

        }

        public bool IsGranted(int roleId, string permissionName)
        {
            var role = _roleRepository.Get(roleId);
            return role.Permissions.Any(T=>T.PermissionName==permissionName);
        }
    }
}
