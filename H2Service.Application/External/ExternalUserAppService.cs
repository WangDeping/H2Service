using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using H2Service.Dto;
using H2Service.External.Dto;
using H2Service.MedicalWastes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.UI;
using H2Service.Authorization;
using H2Service.Users.Dto;
using Abp.Events.Bus;
using H2Service.Events;
using H2Service.Helpers;

namespace H2Service.External
{
 public   class ExternalUserAppService:H2ServiceAppServiceBase,IExternalUserAppService
    {
        private readonly IRepository<ExternalUser> _externalUserRepository;
        private readonly IRepository<User,long> _userRepository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IEventBus _eventBus;
        public ExternalUserAppService(IRepository<ExternalUser> externalUserRepository,
            IRepository<Department> departmentRepository,
            IRepository<User,long> userRepository,
            IEventBus eventBus)
        {
            _externalUserRepository = externalUserRepository;
            _userRepository = userRepository;
            _departmentRepository = departmentRepository;
            _eventBus = eventBus;

        }

        public PagedResultDto<ExternalUserDto> GetPagedExternalUsers(GetExternalUsersInput input)
        {
            var query = _externalUserRepository.GetAll().Where(T=>T.ExternalCompanyId==input.ExternalCompanyId);
            var usersCount = query.Count();
            var usersList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<ExternalUserDto> { Items = usersList.MapTo<List<ExternalUserDto>>(), TotalCount = usersCount };
        }
        public ExternalUserDto GetUser(int Id)
        {
            return _externalUserRepository.Get(Id).MapTo<ExternalUserDto>();

        }
        public string GetCode(int Id)
        {
            return _externalUserRepository.Get(Id).Code;
        }

        public void CreateUser(UserDto input, int departmentId, int remoteDepartmentId)
        {
            if (_userRepository.FirstOrDefault(T => T.UserNumber == input.UserNumber) != null)
                throw new Exception("用户名已存在");
            var user = input.MapTo<User>();
            user.AvatarUrl = null;
            user.Password = SecurityHelper.EncryptMd5(input.TelPhone) ;
            user.Code =user.UserNumber;
            var userEntity = _userRepository.Insert(user);
            var department = _departmentRepository.Get(departmentId);
            department.Users.Add(userEntity);
            _eventBus.Trigger(new CreateUserEventData {
                 AvatarUrl=input.AvatarUrl,
                 Gender=(int)user.Gender,
                 DepartmentId=remoteDepartmentId,
                 TelPhone=user.TelPhone,
                 UserName=user.UserName,
                 UserNumber=user.UserNumber
            });

        }
      

        public void UpdateUser(ExternalUserDto input)
        {
            var user = _externalUserRepository.Get(input.Id);
            ObjectMapper.Map(input, user);
            _externalUserRepository.Update(user);
        }

        public void SetAvatar(int Id, string avatarUrl)
        {
            var user = _externalUserRepository.Get(Id);
            user.AvatarUrl = avatarUrl;
        }

        public string GetAvatar(int Id)
        {
            return _externalUserRepository.Get(Id).AvatarUrl;
        }

        public void RemoveUser(int Id)
        {
            var user = _externalUserRepository.Get(Id);
            if (user == null)
                throw new UserFriendlyException("用户已删除或不存在");
            _externalUserRepository.Delete(Id);
        }

        public GetExternalUserByCodeOutput GetUserByCode(string code)
        {
            var user = _externalUserRepository.FirstOrDefault(T => T.Code == code);
            return user.MapTo<GetExternalUserByCodeOutput>();
        }

       
    }
}
