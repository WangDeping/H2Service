using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Castle.Core.Logging;
using H2Service.Authorization;
using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.DailyUserSsynchronous.Dto;
using H2Service.Helpers;
using H2Service.Log4;
using H2Service.Users;
using H2Service.Users.Dto;
using H2Service.WxWork;
using System.Collections.Generic;
using System.Linq;

namespace H2Service.Hangfire.Jobs.DailyUserSsynchronous
{
    /// <summary>
    /// 每日与企业微信同步通讯录
    /// </summary>
    public class DailyUserSynchronousJob : HangfireJobBase<DailyUserSynchronousJobArgs>
    {
       
        private IRepository<User,long> _userRepository;
        private IUnitOfWorkManager _unitOfWorkManager;
        private ILogAppService _logAppservice;
        private readonly WxUserManager _wxUserManager;
        private const string _defaultPassword = "123456";

        public DailyUserSynchronousJob(
      
          IRepository<User, long> userRepository,
          IUnitOfWorkManager unitOfWorkManager,
          WxUserManager wxUserManager,
          ILogAppService logAppservice)
        {          
            _userRepository = userRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _logAppservice = logAppservice;
            _wxUserManager = wxUserManager;
        }
        [UnitOfWork]
        public override void ExecuteJob(DailyUserSynchronousJobArgs aParams)
        {
            var wxUserList = _wxUserManager.GetWxUsersByDeptId();          
            var usersDto = wxUserList.MapTo<List<CreateUserDto>>();
            usersDto.RemoveAll(t=>Filter(t));//过滤非本院职工
        
            foreach (var userDto in usersDto)
            {   
                using (_unitOfWorkManager.Current.DisableFilter(AbpDataFilters.SoftDelete))
                {
                    var createUser = _userRepository.FirstOrDefault(T => T.UserNumber ==userDto.UserNumber);
                    if (createUser != null)//用户存在
                    {
                        if (createUser.TelPhone != userDto.TelPhone)
                        {
                            createUser.TelPhone = userDto.TelPhone;
                        }
                        continue;
                    }
                     createUser = userDto.MapTo<User>();
                  
                    if (string.IsNullOrEmpty(createUser.TelPhone))
                    {
                        _logAppservice.LogError(string.Format("创建工号失败 {0}姓名为{1}的手机号为空", createUser.UserNumber, createUser.UserName));
                        continue;
                    }
                    createUser.Password = SecurityHelper.EncryptMd5(_defaultPassword);
                    _userRepository.Insert(createUser).MapTo<CreateUserDto>();
                    return;
                }                
                
            }
        }
        private List<CreateUserDto> UserFilter()
        {
            var users = new List<CreateUserDto>();
            users.Add(new CreateUserDto { UserNumber = "CGX" });
            users.Add(new CreateUserDto { UserNumber = "qdwangzj" });
            users.Add(new CreateUserDto { UserNumber = "mlfw" });
            users.Add(new CreateUserDto { UserNumber = "MENGLFWCS2" });
            users.Add(new CreateUserDto { UserNumber = "MENGLFW" });
            users.Add(new CreateUserDto { UserNumber = "MLFW2" });
            users.Add(new CreateUserDto { UserNumber = "CES" }); 
            users.Add(new CreateUserDto { UserNumber = "quyuan" });
            return users;
        }
       
        private  bool Filter(CreateUserDto dto)
        {
            return UserFilter().FirstOrDefault(t => t.UserNumber == dto.UserNumber) != null;
         
        }
    }
}
