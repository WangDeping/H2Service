using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.ServerRooms.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
  public  interface IServerEquipmentAppService: IApplicationService
    {
        PagedResultDto<ServerEquipmentDto> GetPagedServerEquipment(GetServerEquipmentInput input);

       void CreateServerEquipment(CreateServerEquipmentInput input);

       void UpdateServerEquipment(UpdateServerEquipmentInput input);

       void  SetServerAccountAndPassword(ServerSecurityDto dto);

        ServerSecurityDto GetServerSecurityById(int Id);

        ServerEquipmentDto GetServerEquipmentById(int Id);

        IEnumerable<ServerMonitoringDto> GetMonitoringServers();
    }
}
