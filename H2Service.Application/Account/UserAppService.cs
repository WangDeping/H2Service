using System;
using System.Threading.Tasks;
using H2Service.Authorization;
using Abp.Domain.Repositories;
using H2Service.Users.Dto;
using Abp.AutoMapper;
using Abp.UI;
using H2Service.Dto;
using System.Collections.Generic;
using Abp.Collections.Extensions;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Uow;
using H2Service.Account.Dto;
using H2Service.Helpers;
using H2Service.Authorization.Dto;
using Abp.Authorization;
using H2Service.Account;

namespace H2Service.Users
{
    public class UserAppService : H2ServiceAppServiceBase, IUserAppService
    {
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IRepository<UserPermission> _userPermissionRepository;
        private IUnitOfWorkManager _unitOfWorkManager;
        private readonly UserManager _userManager;
        private readonly DepartmentAppService _departmentAppService;
        private const string _defaultPassword = "123456";
        public UserAppService(IRepository<User, long> userRepository,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<Role> roleRepository,
            IRepository<Department> departmentRepository,
             IRepository<UserPermission> userPermissionRepository,
             UserManager userManager,
             DepartmentAppService departmentAppService
             )
        {
            this._userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _roleRepository = roleRepository;
            _departmentRepository = departmentRepository;
            _userPermissionRepository = userPermissionRepository;
            _userManager = userManager;
            _departmentAppService = departmentAppService;
        }
        /// <summary>
        /// 根据工号获取用户
        /// </summary>
        /// <param name="userNumber"></param>
        /// <returns></returns>
        public GetUserByNumberOutput GetUserByNumber(string userNumber)
        {
            var user = _userRepository.FirstOrDefault(T => T.UserNumber == userNumber);
            if (user == null)
                throw new UserFriendlyException("该工号不存在或已被禁用");
            return user.MapTo<GetUserByNumberOutput>();
        }

        public UserDto GetUserById(int Id)
        {
            var user = _userRepository.Single(T => T.Id == Id);
            return user.MapTo<UserDto>();
        }

        public UserDto GetUserByNumberAndPassword(string userNumber, string password)
        {
            var encryptPassword = SecurityHelper.EncryptMd5(password);
            var user = _userRepository.FirstOrDefault(T => T.UserNumber == userNumber && T.Password == encryptPassword);
            
               
            return user.MapTo<UserDto>();

        }
        /// <summary>
        /// 所有用户，包括已经删除
        /// </summary>
        /// <param name="input">查询参数</param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_System_User)]
        public PagedResultDto<DepartmentUserDto> GetPagedUsers(GetUsersInput input)
        {   
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var query = _userRepository.GetAll()
                    .SelectMany(T => T.Departments.DefaultIfEmpty(), (T, S) => new DepartmentUserDto
                    {
                        Id = T.Id,
                        UserName = T.UserName,
                        UserNumber = T.UserNumber,
                        TelPhone = T.TelPhone,
                        Gender = T.Gender,
                        Email = T.Email,
                        DepartmentName = S.DepartmentName == null ? "" : S.DepartmentName,
                        IsDeleted = T.IsDeleted
                    }).WhereIf(!string.IsNullOrEmpty(input.UserNumber), t => t.UserNumber == input.UserNumber)
                   .WhereIf(!string.IsNullOrEmpty(input.UserName), t => t.UserName.Contains(input.UserName))
                   //.WhereIf(!string.IsNullOrEmpty(input.DepartmentName),t=> { input.PageNumber = 0; return input.DepartmentName == "#" ? t.DepartmentName == null : t.DepartmentName.Contains(input.DepartmentName); });
                   .WhereIf(!string.IsNullOrEmpty(input.DepartmentName), t => input.DepartmentName == "#" ? string.IsNullOrEmpty(t.DepartmentName) : t.DepartmentName.Contains(input.DepartmentName));
                var queryCount = query.Count();
                var users = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                return new PagedResultDto<DepartmentUserDto> { Items = users.MapTo<List<DepartmentUserDto>>(), TotalCount = queryCount };
            }

        }

        public List<DepartmentUserDto> GetUsersDepartmentWithDescendants(int depId)
        {

            var departmentIds = _departmentAppService.DepartmentWithDescendants(depId).Select(T=>T.Id);  
            var query = _userRepository.GetAll()
                     .SelectMany(T => T.Departments.DefaultIfEmpty(), (T, S) => new DepartmentUserDto
                     {
                         Id = T.Id,
                         UserName = T.UserName,
                         UserNumber = T.UserNumber,
                         TelPhone = T.TelPhone,
                         Gender = T.Gender,
                         Email = T.Email,
                         Code=T.Code,
                         DepartmentName = S.DepartmentName == null ? "" : S.DepartmentName,
                         DepartmentId=S.DepartmentName==null?0:S.Id,
                         IsDeleted = T.IsDeleted
                     }).Where(T=>departmentIds.Any(D=>D==T.DepartmentId));
            return query.ToList() ;
           

        }

        public List<UserDto> SearchUserName(string userName)
        {
            var users = _userRepository.GetAll().Where(T => T.UserName.Contains(userName));
            return users.MapTo<List<UserDto>>();

        }

        public UserDto GetUserByCode(string code)
        {
            var user = _userRepository.FirstOrDefault(T=>T.Code==code);
            if (user == null)
                throw new UserFriendlyException("该工号不存在或已被禁用");
            return user.MapTo<UserDto>();

        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="createdto"></param>
        /// <returns></returns>       
        public void CreateUser(CreateUserDto createdto)
        {
            using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
            {
                var user = _userRepository.FirstOrDefault(T => T.UserNumber == createdto.UserNumber);
                if (user != null)
                    return;
                var createUser = createdto.MapTo<User>();
                if (string.IsNullOrEmpty(createUser.TelPhone))
                {
                    Logger.Error(string.Format("创建工号失败 {0}姓名为{1}的手机号为空", createUser.UserNumber, createUser.UserName));
                    return;
                }
                createUser.Password = SecurityHelper.EncryptMd5(_defaultPassword);
                var createUserDto = _userRepository.Insert(createUser).MapTo<CreateUserDto>();
                return;
            }
        }

        /// <summary>
        /// 修改用户信息
        /// </summary>
        /// <param name="input"></param>
        public void UpdateUser(UpdateUserInput input)
        {
            var user = _userRepository.Get(input.Id);
            if (user == null)
                throw new UserFriendlyException(-1, "用户不存在");
            ObjectMapper.Map(input, user);
            _userRepository.Update(user);


        }

        public void ResetPassword(int Id)
        {
            var user = _userRepository.Get(Id);
            if (user == null)
                throw new UserFriendlyException(-1, "用户不存在");
            user.Password = SecurityHelper.EncryptMd5(_defaultPassword);
        }

        public void RemoveUser(int Id)
        {
            _userManager.DeleteUser(Id);

        }


        /// <summary>
        /// 获取用户下所属角色
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<RoleDto> GetPagedRoles(GetPagedRolesInUserInput input)
        {
            var query = _userRepository.Get(input.UserId).Roles;
            if (query == null)
            {
                return new PagedResultDto<RoleDto> { Items = new List<RoleDto>(), TotalCount = 0 };
            }
            else
            {
                var queryCount = query.Count();
                var roles = query.OrderBy(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
                return new PagedResultDto<RoleDto> { Items = roles.MapTo<List<RoleDto>>(), TotalCount = queryCount };
            }


        }

        public void AddRole(RoleUserDto dto)
        {
            var user = _userRepository.Get(dto.UserId);
            if (user.Roles.FirstOrDefault(T => T.Id == dto.RoleId) == null)
                user.Roles.Add(_roleRepository.Get(dto.RoleId));
            else
                throw new UserFriendlyException("该用户已经分配该角色");
        }

        public void RemoveRole(RoleUserDto dto)
        {
            var role = _roleRepository.Get(dto.RoleId);
            var user = _userRepository.Get(dto.UserId);
            user.Roles.Remove(role);
        }

        public void AddDepartment(DepartmentUserDto dto)
        {
            var user = _userRepository.Get(dto.UserId);         
            if (user.Departments.Count > 0)
                throw new UserFriendlyException("用户不能同时兼职两个部门");
            else
            {
                var department = _departmentRepository.Get(dto.DepartmentId);
                if(department.IsAllowedHasUser)
                    user.Departments.Add(department);
                else
                    throw new UserFriendlyException("此部门下不允许存在用户");
            }
        }

        public void RemoveDepartment(DepartmentUserDto dto)
        {
            var user = _userRepository.Get(dto.UserId);
            var department = _departmentRepository.Get(dto.DepartmentId);
            user.Departments.Remove(department);
        }
        public DepartmentDto GetDefaultDepartment(int userId)
        {
            var department = _userRepository.Get(userId).Departments.FirstOrDefault();
            return ObjectMapper.Map<DepartmentDto>(department);

        }
        [AbpAuthorize(PermissionNames.Pages_System_Permission)]
        public void GrantPermission(GrantPermissionInput input)
        {
            _userPermissionRepository.Delete(T => T.UserId == input.UserId);
            var user = _userRepository.Get((long)input.UserId);  
            foreach (var permission in input.Permissions)
            {             
                user.Permissions.Add(new UserPermission { PermissionName = permission, UserId = user.Id });
            }

        }

        public List<UserDto> FindUser(FindUserInput input) {
            var result = _userRepository.GetAll().WhereIf(!string.IsNullOrEmpty(input.PermissionName), 
                T => T.Permissions.Any(P => P.PermissionName == input.PermissionName)||
                T.Roles.Any(R=>R.Permissions.Any(P=>P.PermissionName==input.PermissionName))||
                T.Departments.Any(D=>D.Permissions.Any(P=>P.PermissionName==input.PermissionName))).ToList();
            return result.MapTo<List<UserDto>>();

        }

    }


}
