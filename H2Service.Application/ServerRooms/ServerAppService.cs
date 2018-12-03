using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    public class ServerAppService : H2ServiceAppServiceBase, IServerAppService
    {
        
        public ServerAppService()
        {
          

        }
        public string GetServerInfo()            
        {
            var _serverWMICoreManager = new ServerWMICoreManger("192.168.253.20","Administrator","by@dhcc=2012");
            return string.Join("$", _serverWMICoreManager.GetLogicDiskInfo());
        }
    }
}
