using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    /// <summary>
    /// 服务器或者设备
    /// </summary>
 public   class ServerEquipment:Entity<int>, ISoftDelete
    {
        [Required]        
        public string SEName { get; set; }

        public string IP { get; set; }

        public string Functions { get; set; }

        /// <summary>
        /// 设备类型
        /// </summary>
        public SEType? SEType { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string SEModel { get; set; }

        public OperatingSystem? OperatingSystem { get; set; }

        public DateTime? PurchaseDate { get; set; } 
        /// <summary>
        /// 质保期(年)
        /// </summary>
        public int? QualityGuaranteePeriod { get; set; }

        public int? ServerRoomID { get; set; }

        [ForeignKey("ServerRoomID")]
        public virtual ServerRoom ServerRoom { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        //登录用账户
        public string Account { get; set; }

        //登录用密码
        public string Password { get; set; }

        //是否需要监控
        [DefaultValue(false)]
        public bool IsMonitored { get; set; }
    }

    public enum SEType
    {       
       服务器,       
       虚拟机,
       存储,
       交换机,
       负载均衡,
       空调,
       UPS,      
       消防器材,
       其他
    }

    public enum OperatingSystem {
        
        WindowsServer2003,
        WindowsServer2008,      
        WindowsServer2012,
        RedHat,
        Linux
    }
}
