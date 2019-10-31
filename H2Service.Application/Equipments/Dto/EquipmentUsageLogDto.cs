using Abp.AutoMapper;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
   [AutoMap(typeof(EquipmentUsageLog))]
   public  class EquipmentUsageLogDto
    {
        public int Id { get; set; }
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
        
        public User BeginUser { get; set; }
        public DateTime? EndTime { get; set; }
        public long? EndUserId { get; set; }
       
        public User EndUser { get; set; }
    }
}
