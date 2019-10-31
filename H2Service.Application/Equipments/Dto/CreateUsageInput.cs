using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentUsageLog))]
    public   class CreateUsageInput
    {
        /// <summary>
        /// 设备
        /// </summary>
        public int EquipmentId { get; set; }
        /// <summary>
        /// 患者信息
        /// </summary>
        public string PatientAdmNo { get; set; }
        public string PatientName { get; set; }
        public string Diagnose { get; set; }
        /// <summary>
        /// 开始结束记录
        /// </summary>
        public DateTime? BeginTime { get; set; }
        public long? BeginUserId { get; set; }
    }
}
