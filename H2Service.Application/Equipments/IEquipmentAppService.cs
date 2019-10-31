using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public   interface IEquipmentAppService:IApplicationService
    {
        int CreateEquipment(CreateEquipmentInput input);
        void UpdateEquipment(CreateEquipmentInput input);
        PagedResultDto<EquipmentOutput> GetPagedEquipment(GetEquipmentInput input);
        EquipmentOutput GetEquipment(GetEquipmentInput input);
        IList<EquipmentOutput> GetNeedPatrolEquipment(GetNeedPatrolEquipmentInput input);
    }
}
