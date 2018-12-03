using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Services;
using H2Service.Authorization.Departments;
using H2Service.Authorization.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
    public class PermissionDomainService : DomainService, IPermissionDomainService
    {
        private readonly IPermissionManager _permissionManager;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserPermission> _userPermissionRepository;
        private readonly IRepository<DepartmentPermission> _departmentPermissionRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
       //private readonly IDepartmentDomainService _departmentDomainService;

        public PermissionDomainService(IPermissionManager permissionManager, 
            IRepository<User, long> userRepository,
            IRepository<UserPermission> userPermissionRepository,
             IRepository<DepartmentPermission> departmentPermissionRepository,
             IRepository<Department> departmentRepository,
             IRepository<Role> roleRepository,
             IRepository<RolePermission> rolePermissionRepository
             )
        {
            _permissionManager = permissionManager;
            _userRepository = userRepository;
            _userPermissionRepository = userPermissionRepository;
            _departmentPermissionRepository = departmentPermissionRepository;
            _departmentRepository = departmentRepository;
            _rolePermissionRepository = rolePermissionRepository;
            _roleRepository = roleRepository;
        }
        public IEnumerable<UserPermission> GetPermissionByUser(long userId)
        {
            var user = _userRepository.Get(userId);
            return user.Permissions;
        }
        public IEnumerable<RolePermission> GetPermissionByRole(int roleId)
        {
            return _roleRepository.Get(roleId).Permissions;
        }
        public IEnumerable<DepartmentPermission> GetPermissionByDepartment(int depId)
        {
            return _departmentRepository.Get(depId).Permissions;
        }

        public List<Permission> GetAllPermissions()
        {

            var permissions = PermissionFinder.GetAllPermissions(new H2ServiceAuthorizationProvider()).ToList();
            return permissions;
        }

        public Permission GetPermission(string name = null)
        {
            if (name == null)
                return _permissionManager.GetPermission(PermissionNames.Pages);
            else
                return _permissionManager.GetPermission(name);

        }

        /// <summary>
        /// 分配用户级权限(先删除原先权限，再重新赋予新权限)
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="permissions">权限</param>
        public  void AssignPermissionToUser(long userId, List<Permission> permissions)
        {
            _userPermissionRepository.Delete(T=>T.UserId==userId);
            var user = _userRepository.Get(userId);
            user.Permissions = new List<UserPermission>();
            foreach (var permission in permissions)
                user.Permissions.Add(new UserPermission {  PermissionName=permission.Name, UserId=userId});
        }

        /// <summary>
        /// 分配部门级权限(先删除原先权限，再重新赋予新权限,不区分上下级部门,即分配权限时仅对当前部门起作用)
        /// </summary>
        /// <param name="depId"></param>
        /// <param name="permissions"></param>
        public void AssignPermissionToDepartment(int depId, List<Permission> permissions)
        {
            _departmentPermissionRepository.Delete(T => T.DepartmentId == depId);
            var department = _departmentRepository.Get(depId);
            department.Permissions = new List<DepartmentPermission>();
            foreach (var permission in permissions)
                department.Permissions.Add(new DepartmentPermission { PermissionName = permission.Name, DepartmentId=depId });

        }

        public void AssignPermissionToRole(int roleId, List<Permission> permissions)
        {
            _rolePermissionRepository.Delete(T => T.RoleId== roleId);
            var role = _roleRepository.Get(roleId);
            role.Permissions = new List<RolePermission>();
            foreach (var permission in permissions)
                role.Permissions.Add(new RolePermission { PermissionName = permission.Name, RoleId=roleId });

        }
    }
}
