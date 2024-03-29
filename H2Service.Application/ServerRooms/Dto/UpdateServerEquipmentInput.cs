﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerEquipment))]
    public   class UpdateServerEquipmentInput
    {
        public int Id { get; set; }

        public string SEName { get; set; }

        public string IP { get; set; }

        public string Functions { get; set; }

        public string SEType { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public string SEModel { get; set; }

        public string OperatingSystem { get; set; }

        public DateTime? PurchaseDate { get; set; }
        /// <summary>
        /// 质保期(年)
        /// </summary>
        public int? QualityGuaranteePeriod { get; set; }       


        public int ServerRoomId { get; set; }

        public int ServerRoomName { get; set; }
        public string Description { get; set; }


    }
}
