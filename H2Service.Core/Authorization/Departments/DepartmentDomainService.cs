using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Departments
{
    public class DepartmentDomainService : DomainService, IDepartmentDomainService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<DepartmentPermission> _departmentPermissionRepository;       
        
        public DepartmentDomainService(IRepository<User, long> userRepository, 
            IRepository<Department> departmentRepository,
            IRepository<DepartmentPermission> departmentPermissionRepository)
        {
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _departmentPermissionRepository = departmentPermissionRepository;
            
        }
        /// <summary>
        /// 关联用户与科室
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="departmentId">部门Id</param>
        public void AssginDepartment(long userId, int departmentId)
        {
            var user = _userRepository.Get(userId);
            _departmentRepository.Get(departmentId).Users.Add(user);
        }

        public void DeleteDepartmentUser(long userId, int departmentId)
        {
            var user = _userRepository.Get(userId);
            _departmentRepository.Get(departmentId).Users.Remove(user);
        }
        /// <summary>
        /// 删除部门(级联删除用户-部门关系,删除权限)
        /// </summary>
        /// <param name="departmentId"></param>
        public void DeleteDepartmentUser(int departmentId)
        {
            var users = _departmentRepository.Get(departmentId).Users.ToList();          
            for (int i = users.Count-1; i >= 0; i--) {

                users.RemoveAt(i);
            }
            _departmentPermissionRepository.Delete(T=>T.DepartmentId==departmentId);
            _departmentRepository.Delete(departmentId);
        }



        public bool IsGranted(int departmentId, string permissionName)
        {
            var department = _departmentRepository.Get(departmentId);
            return department.Permissions.Any(T => T.PermissionName == permissionName);

        }
    }
}
