using Abp.Domain.Repositories;
using Abp.Domain.Services;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
    public class AuthorizationManager : DomainService, IAuthorizationManager
    {
        //private IRepository<User, long> _userRepository;
        //private IRepository<Role> _roleRepository;
        //private IDepartmentDomainService _dpartmentDomainService;
        //public AuthorizationManager(IRepository<User, long> userRepository
        //    , IRepository<Role> roleRepository, IDepartmentDomainService dpartmentDomainService)
        //{
        //    _userRepository = userRepository;
        //    _roleRepository = roleRepository;
        //    _dpartmentDomainService = dpartmentDomainService;
        //}
       

        //public string GetAssignedPermissonStringByUser(long userId)
        //{
        //    IEnumerable<H2Permission> list = this.GetAssignedPermissonByUser(userId);
        //    StringBuilder builder = new StringBuilder();
        //    foreach (var permission in list)
        //        builder.Append(permission.PermissionName+";");
        //    return builder.ToString();
        //}
    }
}
