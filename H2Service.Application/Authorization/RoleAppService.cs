using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Authorization.Dto;
using H2Service.Authorization.Roles;
using H2Service.Dto;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
 public   class RoleAppService:H2ServiceAppServiceBase,IRoleAppService
    {
        private readonly IRepository<Role> _roleRepository;
        private readonly IRoleDomainService _roleDomainService;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<RolePermission> _rolePermissionRepository;
        public RoleAppService(IRepository<Role> roleRepository,
            IRoleDomainService roleDomainService,
             IRepository<User, long> userRepository,
             IRepository<RolePermission> rolePermissionRepository
            )
        {
            _roleRepository = roleRepository;
            _roleDomainService = roleDomainService;
            _userRepository = userRepository;
            _rolePermissionRepository = rolePermissionRepository;
        }
        [AbpAuthorize(PermissionNames.Pages_System_Role)]
        public PagedResultDto<RoleDto> GetPagedRoles(GetRolesInput input)
        {
            var query = _roleRepository.GetAll();
            var queryCount = query.Count();
            var roles = query.OrderBy(T=>T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<RoleDto> { Items = roles.MapTo<List<RoleDto>>(), TotalCount = queryCount };

        }
        /// <summary>
        /// 获取该角色下所有用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_System_Role)]
        public PagedResultDto<UserDto> GetPagedUsersInRole(GetPagedUsersInRoleInput input)
        {
            var query = _roleRepository.Get(input.RoleId).Users;
            if (query == null)
            {
                return new PagedResultDto<UserDto> { Items = new List<UserDto>(), TotalCount = 0 };
            }
            else {

                var queryCount = query.Count();
                var users=query.OrderBy(T=>T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                return new PagedResultDto<UserDto> { Items = users.MapTo<List<UserDto>>(), TotalCount = queryCount };
            }
               

        }
        [AbpAuthorize(PermissionNames.Pages_System_Role)]
        public void CreateRole(RoleDto dto)
        {
            if(_roleRepository.FirstOrDefault(R=>R.RoleName==dto.RoleName)!=null)
                throw new   UserFriendlyException("相同名称角色已经存在");
            _roleRepository.Insert(ObjectMapper.Map<Role>(dto));
        }
        [AbpAuthorize]
        public RoleDto GetRole(int Id)
        {
            return _roleRepository.Get(Id).MapTo<RoleDto>();
        }
        [AbpAuthorize(PermissionNames.Pages_System_Role)]
        public void UpdateRole(RoleDto dto)
        {
            _roleRepository.Update(ObjectMapper.Map<Role>(dto));

        }
        [AbpAuthorize(PermissionNames.Pages_System_Role)]
        public void RemoveRole(int Id)
        {
            _roleDomainService.RemoveRoleWithUsersAndPermission(Id);
        }

        [AbpAuthorize(PermissionNames.Pages_System_Permission)]
        public void GrantPermission(GrantPermissionInput input)
        {
            _rolePermissionRepository.Delete(T => T.RoleId == input.RoleId);
            var role = _roleRepository.Get((int)input.RoleId);
            foreach (var permission in input.Permissions)
            {
                role.Permissions.Add(new RolePermission { PermissionName = permission,  RoleId=role.Id });
            }

        }
    }
}
