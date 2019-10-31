using Abp.Domain.Entities;
using H2Service.Authorization;
using H2Service.EnumDic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    public class Equipment : Entity,ISoftDelete
    {
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 设备分类
        /// </summary>
        public int EquipmentKindId { get; set; }
        [ForeignKey("EquipmentKindId")]
        public virtual EquipmentKind Kind { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int EquipmentTypeId { get; set; }
        [ForeignKey("EquipmentTypeId")]
        public virtual EquipmentType Type { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public int EquipmentModelId { get; set; }
        [ForeignKey("EquipmentModelId")]
        public virtual EquipmentModel Model { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Dept { get; set; }
        /// <summary>
        /// 设备状态
        /// </summary>
        public EquipmentStatus Status { get; set; }
        /// <summary>
        /// 设备图片
        /// </summary>
        public string EquipmentImg { get; set; }
        /// <summary>
        /// 使用记录
        /// </summary>
        public virtual ICollection<EquipmentUsageLog> UsageLogs { get; set; }
        /// <summary>
        /// 巡视记录
        /// </summary>
        public virtual ICollection<EquipmentPatrolLog> PatrolLogs { get; set; }
        /// <summary>
        /// 借用记录
        /// </summary>
        public virtual ICollection<EquipmentLoanLog> LoanLogs { get; set; }
        public bool IsDeleted { get; set; }
    }
  
}
