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
namespace H2Service.External
{
 public   class ExternalUserAppService:H2ServiceAppServiceBase,IExternalUserAppService
    {
        private readonly IRepository<ExternalUser> _externalUserRepository;

        public ExternalUserAppService(IRepository<ExternalUser> externalUserRepository)
        {
            _externalUserRepository = externalUserRepository;
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
        public void CreateUser(ExternalUserDto input)
        {
            var user = input.MapTo<ExternalUser>();
            user.Code =new Random().Next(10000000, 99999999).ToString()+new Random().Next(1000,9999);
            _externalUserRepository.Insert(user);
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
