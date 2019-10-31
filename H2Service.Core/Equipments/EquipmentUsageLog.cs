using Abp.Domain.Entities;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    /// <summary>
    /// 设备使用记录
    /// </summary>
 public   class EquipmentUsageLog:Entity
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
        [ForeignKey("BeginUserId")]
        public User BeginUser { get; set; }
        public DateTime? EndTime { get; set; }
        public long? EndUserId { get; set; }
        [ForeignKey("EndUserId")]
        public User EndUser { get; set; }

    }
}
