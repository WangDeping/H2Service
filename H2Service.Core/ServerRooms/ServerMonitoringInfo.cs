using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    public class ServerMonitoringInfo
    {
     public  IEnumerable<LogicDiskInfo> DisksList { get; set; }

     public bool ConnectStatus { get; set; }
    }

    public class LogicDiskInfo
    {
        public string Name { get; set; }
        /// <summary>
        /// 总空间大小GB
        /// </summary>
        public double Size { get; set; }
        /// <summary>
        /// 剩余空间大小
        /// </summary>
        public double FreeSpace { get; set; }
        /// <summary>
        /// 已使用空间大小
        /// </summary>
        public double UsageSpace { get { return Size - FreeSpace; } }

        public double Utilization { get { return UsageSpace / Size*100; } }

    }
   
}
