using Abp.Collections.Extensions;
using H2Service.Account;
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

namespace H2Service.Hangfire.Jobs.DailyEquipments
{
 public   class DaliyEquipmentsNotifyJob: HangfireJobBase<NoneJobParam>
    {
        private readonly IEquipmentAppService _equipmentAppService;
        private readonly ILogAppService _logAppservice;
        private readonly WxSender _wxSender;
        private readonly IDepartmentAppService _departmentAppService;
        public DaliyEquipmentsNotifyJob(IEquipmentAppService equipmentAppService,
            ILogAppService logAppservice, WxSender wxSender, IDepartmentAppService departmentAppService) {
            _equipmentAppService = equipmentAppService;
            _logAppservice = logAppservice;
            _wxSender = wxSender;
            _departmentAppService = departmentAppService;

        }

        public override void ExecuteJob(NoneJobParam aParams)
        {
            var input = new GetNeedPatrolEquipmentInput
            {
                Freq = EquipmentPartrolFrequencyEnum.每日,
                NotPatrol = true,
                PatrolType = PatrolTypeEnum.I级巡视,
                Status = H2Service.EnumDic.EquipmentStatus.完好
            };
            var equipments = _equipmentAppService.GetNeedPatrolEquipment(input);
            _logAppservice.LogError("数量"+equipments.Count);
            foreach (var eq in equipments) {
                var content = string.Format("{0}-{1}-{2}没有I级巡检({3}----{4})",eq.EquipmentModelName,eq.EquipmentTypeName,eq.Code,eq.DepartmentName,DateTime.Now.Date.ToShortDateString());
                _logAppservice.LogError(content);              
                var toUser = _departmentAppService.GetUserByDepartmentId(eq.DepartmentId).Select(T => T.UserNumber).JoinAsString("|");
                _logAppservice.LogError(toUser);
                var msg = new WxSendTextMsg(content,toUser );
                msg.agentid = WebConfigurationManager.AppSettings["equipmentAppid"];
                _wxSender.SendMsg(msg);
            }
        }
    }
}
