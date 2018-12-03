using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
 public  interface IServerWMICoreManger:IDomainService
    {
        IEnumerable<string> GetLogicDiskInfo();
    }
}
