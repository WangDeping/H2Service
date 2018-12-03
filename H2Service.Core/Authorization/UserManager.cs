using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using H2Service.Authorization.Departments;
using H2Service.Authorization.Roles;
using H2Service.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
 public   class UserManager: DomainService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IDepartmentDomainService _departmentManager;
        private readonly IRoleDomainService _roleManager;
        private readonly IEventBus _eventBus;
        public UserManager(IRepository<User, long> userRepository,
           IDepartmentDomainService departmentManager,
           IRoleDomainService roleManager,
           IEventBus eventBus)
        {
            _userRepository = userRepository;
            _departmentManager = departmentManager;
            _roleManager = roleManager;
            _eventBus = eventBus;
        }
        public void DeleteUser(long Id)
        {
            var user = _userRepository.Get(Id);        
            _userRepository.Delete(user);
            _eventBus.Trigger(new DeleteUserEventData { UserNumber=user.UserNumber });
        }

        public void DeleteUser(string userNumber)
        {
            var user = _userRepository.GetAll().Where(T=>T.UserNumber==userNumber).FirstOrDefault();           
            _userRepository.Delete(user);
            _eventBus.Trigger(new DeleteUserEventData { UserNumber = user.UserNumber });


        }

        [UnitOfWork]
        public virtual bool IsGranted(long userId, string permissionName)
        {
            var user = _userRepository.Get(userId);
            if (user.Permissions.Any(T => T.PermissionName == permissionName))
                return true;
            var departments = user.Departments;
            foreach (var dep in departments)
                if (_departmentManager.IsGranted(dep.Id, permissionName))
                    return true;
            var roles = user.Roles;
            foreach (var role in roles)
                if (_roleManager.IsGranted(role.Id, permissionName))
                    return true;
            return false;
        }
        
    }
}
