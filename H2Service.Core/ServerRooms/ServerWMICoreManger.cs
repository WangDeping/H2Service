using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
 public   class ServerWMICoreManger
    {
        private ManagementScope scope;
        public ServerMonitoringInfo server;
        public ServerWMICoreManger(string host,string account,string password)
        {           
            string serverString= @"\\" + host + @"\root\cimv2";
            this.scope = new ManagementScope(serverString);
            ConnectionOptions options = new ConnectionOptions
            {
                Username =account,
                Password =password,
                Impersonation = ImpersonationLevel.Impersonate,
                Authentication = AuthenticationLevel.Packet,
                 Authority= "ntlmdomain:domain"
                //  Timeout=TimeSpan.FromSeconds(15)
            };
           scope.Options = options;
           server = new ServerMonitoringInfo();
            try
            {
                scope.Connect();
                server.DisksList = this.GetLogicDiskInfo();
                server.ConnectStatus = true;
            }
            catch
            {
                server.ConnectStatus = false;
                server.DisksList = new List<LogicDiskInfo>();
            }
        }

        public IEnumerable<LogicDiskInfo> GetLogicDiskInfo()
        {
            List<LogicDiskInfo> disks = new List<LogicDiskInfo>();
            try
            {
                long GB = 1024 * 1024 * 1024;
               
                SelectQuery query = new SelectQuery("select * from Win32_LogicalDisk where DriveType=3 or DriveType=4 ");
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);
                foreach (ManagementBaseObject disk in searcher.Get())
                {
                    var serverDisk = new LogicDiskInfo();
                    serverDisk.FreeSpace = Convert.ToDouble(disk["FreeSpace"]) / GB;
                    serverDisk.Name = disk["Name"].ToString();
                    serverDisk.Size = Convert.ToDouble(disk["Size"]) / GB;
                    disks.Add(serverDisk);

                }
                return disks;
            }
            catch {
                return disks;
            }
          
        }
       
    }
}
