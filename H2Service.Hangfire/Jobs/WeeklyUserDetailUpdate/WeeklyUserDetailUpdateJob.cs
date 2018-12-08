using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using H2Service.Authorization;
using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.WeeklyUserDetailUpdate.Dto;
using H2Service.Helpers;
using H2Service.Log4;
using H2Service.WxWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace H2Service.Hangfire.Jobs.WeeklyUserDetailUpdate
{
    /// <summary>
    /// 更新用户详细信息(头像)
    /// </summary>
    public class WeeklyUserDetailUpdateJob : HangfireJobBase<WeeklyUserDetailUpdateJobArgs>
    {

        private readonly IRepository<User, long> _userRepository;
        private readonly WxUserManager _wxUserManager;        
        private ILogAppService _logAppservice;
        public WeeklyUserDetailUpdateJob(IRepository<User, long> userRepository,           
            ILogAppService logAppService,
            WxUserManager wxUserManager)
        {
            _userRepository = userRepository;
         
            _logAppservice = logAppService;
            _wxUserManager= wxUserManager;
        }
        [UnitOfWork]
        public override void ExecuteJob(WeeklyUserDetailUpdateJobArgs aParams)
        {
            var users = _userRepository.GetAll().OrderBy(T=>T.Id);//.GetAllList();
            var helper = new DownLoadHelper();
            foreach (var user in users)
            {
                var wxDetail = _wxUserManager.GetWxUserBaseInfo(user.UserNumber);
                if (wxDetail != null)
                {
                    // _logAppservice.LogError(wxDetail.name+":"+wxDetail.avatar);
                    Task.Run(()=> { helper.DownloadAvatar(wxDetail.avatar, user.UserNumber);  });
                    
                    user.AvatarUrl = wxDetail.avatar;
                    user.Gender = (Gender)int.Parse(wxDetail.gender);
                }

            }
        }
    }
}
