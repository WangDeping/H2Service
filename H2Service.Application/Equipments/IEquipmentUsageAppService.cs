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
 public   interface IEquipmentUsageAppService: IApplicationService
    {
        void CreateUsage(CreateUsageInput input);
        PagedResultDto<EquipmentUsageLogDto> GetPatrolLogs(GetUsagelLogsInput input);
        void EndUsage(EndUsageInput input);
    }
}
