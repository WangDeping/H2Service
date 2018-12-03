using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    /// <summary>
    /// 机房巡视
    /// </summary>
    public class ServerRoomPatrol : Entity<int>, ICreationAudited
    {
        /// <summary>
        /// 温度
        /// </summary>
        public int Temperature { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public int Humidity { get; set; }

        /// <summary>
        /// 是否清洁
        /// </summary>
        public bool IsClean { get; set; }

        /// <summary>
        /// 是否有异响
        /// </summary>
        public bool HasNoise { get; set; }

        /// <summary>
        /// 是否有异味
        /// </summary>
        public bool HasOdor { get; set; }

        /// <summary>
        /// 是否故障灯亮
        /// </summary>
        public bool HasWarningLight { get; set; }

        /// <summary>
        /// UPS是否正常
        /// </summary>
        public bool IsUPSWorking { get; set; }

        /// <summary>
        /// 空调是否正常
        /// </summary>
        public bool IsAirConditionerWorking { get; set; }

        /// <summary>
        /// 消防设备是否正常
        /// </summary>
        public bool IsFireFightingDeviceWorking { get; set; }
        /// <summary>
        /// 整体检查小结
        /// </summary>
        [Required]
        public string RoomSummary { get; set; }      
        

        public int ServerRoomId { get; set; }
        /// <summary>
        /// 机房
        /// </summary>
        [ForeignKey("ServerRoomId")]
        public virtual ServerRoom ServerRoom { get; set; }
        /// <summary>
        /// 巡视图片存放路径
        /// </summary>
        public string ImgsPath { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }


        public long? CreatorUserId { get ; set ; }

        public DateTime CreationTime { get ; set ; }

       
    }
}
