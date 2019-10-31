using H2Service.EnumDic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
 public   class GetNeedPatrolEquipmentInput
    {
        /// <summary>
        /// 巡视频率
        /// </summary>
        public EquipmentPartrolFrequencyEnum Freq { get; set; }
        /// <summary>
        /// 巡视级别类型
        /// </summary>
        public PatrolTypeEnum PatrolType { get; set; }
        /// <summary>
        /// 是否查看未巡视 True查看,False不区分
        /// </summary>
        public bool NotPatrol { get; set; }
        /// <summary>
        /// 设备好坏
        /// </summary>
        public EquipmentStatus Status { get; set; }
     
    }
}
