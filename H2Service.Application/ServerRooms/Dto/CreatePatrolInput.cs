﻿using Abp.AutoMapper;
using System;
using System.ComponentModel.DataAnnotations;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerRoomPatrol))]
    public   class CreatePatrolInput
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
        
        public string RoomSummary { get; set; }
       
        public int ServerRoomId { get; set; }
        /// <summary>
        /// 机房
        /// </summary>
      
        /// <summary>
        /// 巡视图片存放路径
        /// </summary>
        public string ImgsPath { get; set; }       


        //public long? CreatorUserId { get; set; }

        //public DateTime CreationTime { get; set; }
    }
}
