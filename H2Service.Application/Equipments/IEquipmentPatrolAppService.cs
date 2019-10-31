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
 public  interface IEquipmentPatrolAppService: IApplicationService
    {
        void CreatePatrol(CreatePatrolInput input);
        PagedResultDto<EquipmentPatrolLogDto> GetPatrolLogs(GetPatrolLogsInput input);
    }
}
