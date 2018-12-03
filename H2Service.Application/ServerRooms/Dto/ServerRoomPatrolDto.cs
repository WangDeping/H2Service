using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerRoom))]
    public   class ServerRoomPatrolDto
    {
        public int Id { get; set; }
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
        /// 机房名称标识
        /// </summary>
        public string ServerRoomName { get; set; }
        /// <summary>
        /// 机房
        /// </summary>

        /// <summary>
        /// 巡视图片存放路径
        /// </summary>
        public string ImgsPath { get; set; }


        public long? CreatorUserId { get; set; }

        /// <summary>
        /// 巡视人
        /// </summary>
        public string CreatorName { get; set; }

        public string CreationTime { get; set; }

    }
}
