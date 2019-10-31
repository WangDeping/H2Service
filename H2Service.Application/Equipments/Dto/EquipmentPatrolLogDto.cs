using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentPatrolLog))]
    public   class EquipmentPatrolLogDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public int EquipmentId { get; set; }      
        public string EquipmentName { get; set; }
        /// <summary>
        /// 巡视类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 设备类型名称
        /// </summary>
        public string EquipmentTypeName { get; set; }

        public bool MainCheck { get; set; }
        public string Summary { get; set; }
        public string CreatorUserName { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
