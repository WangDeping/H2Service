using Abp.Collections.Extensions;
using H2Service.Account;
using H2Service.Authorization;
using H2Service.Equipments;
using H2Service.Equipments.Dto;
using H2Service.Hangfire.Framework;
using H2Service.Log4;
using H2Service.Users;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.Hangfire.Jobs.MonthlyEquipments
{
    /// <summary>
    /// II级巡视每月提醒
    /// </summary>
    public   class MothlyEquipmentsNotifyJobII: HangfireJobBase<NoneJobParam>
    {
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly ILogAppService _logAppservice;
        private readonly WxSender _wxSender;
        private readonly IUserAppService _userAppService;

        public MothlyEquipmentsNotifyJobII(IEquipmentAppService equipmentAppService,
            ILogAppService logAppservice, WxSender wxSender, IUserAppService userAppService) {
            _equipmentAppService = equipmentAppService;
            _logAppservice = logAppservice;
            _wxSender = wxSender;
            _userAppService = userAppService;

        }

        public override void ExecuteJob(NoneJobParam aParams)
        {
            var input = new GetNeedPatrolEquipmentInput
            {
                Freq = EquipmentPartrolFrequencyEnum.每月,
                NotPatrol = false,
                PatrolType = PatrolTypeEnum.II级巡视,
                Status = H2Service.EnumDic.EquipmentStatus.完好
            };
            var equipments = _equipmentAppService.GetNeedPatrolEquipment(input);//需巡检设备
            _logAppservice.LogError("数量" + equipments.Count);
            var content = string.Format("截至{0}-II级巡检应巡{1}台,巡检完成{2}台", DateTime.Now.Date.ToShortDateString(), 
                equipments.Count, equipments.Where(T=>T.HasPatrol_II).Count());
            _logAppservice.LogError(content);
            var toUser = _userAppService.FindUser(new Account.Dto.FindUserInput { PermissionName = PermissionNames.Pages_Equipment_PatrolII }).Select(T => T.UserNumber).JoinAsString("|");
            _logAppservice.LogError(toUser);          
            var msg = new WxSendNewsMsg("详细信息进入查看",
               WebConfigurationManager.AppSettings["extranet"] + "Content/SharedImgs/equipmentBg.jpg", content,
               WebConfigurationManager.AppSettings["appBaseUrl"] + "Equipment2/NotPatrolView/",
               toUser,
                WebConfigurationManager.AppSettings["equipmentAppid"]);
            _wxSender.SendMsg(msg);
        }
    }
        
}
