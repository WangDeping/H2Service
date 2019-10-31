using Abp.Web.Mvc.Authorization;
using App.Models.Equipment;
using H2Service.Authorization;
using H2Service.Equipments;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Controllers
{
    [AbpMvcAuthorize(PermissionNames.Pages_Equipment_PatrolII)]

    public class Equipment2Controller : AppControllerBase
    {
        private readonly IEquipmentPatrolAppService _patrolAppService;
        private readonly IEquipmentAppService _equipmentAppService;
        public Equipment2Controller(IEquipmentPatrolAppService patrolAppService,
             IEquipmentAppService equipmentAppService) {
            _patrolAppService = patrolAppService;
            _equipmentAppService = equipmentAppService;
        }

        // GET: Equipment2
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotPatrolView() {
            var input = new GetNeedPatrolEquipmentInput
            {
                Freq = EquipmentPartrolFrequencyEnum.每月,
                NotPatrol = false,
                PatrolType = PatrolTypeEnum.II级巡视,
                Status = H2Service.EnumDic.EquipmentStatus.完好
            };
            var input2 = new GetNeedPatrolEquipmentInput
            {
                Freq = EquipmentPartrolFrequencyEnum.每月,
                NotPatrol = false,
                PatrolType = PatrolTypeEnum.II级巡视,
                Status = H2Service.EnumDic.EquipmentStatus.维修
            };
            var notpatrol = _equipmentAppService.GetNeedPatrolEquipment(input);
            var notwork = _equipmentAppService.GetNeedPatrolEquipment(input2);
            var model = new PatrolIINotifyModel();
            model.HasPatrol = notpatrol.Where(T => T.HasPatrol_II);
            model.NotPatrol = notpatrol.Where(T =>!T.HasPatrol_II);
            model.NotWork = notwork;
            return View(model);
        }
    }
}