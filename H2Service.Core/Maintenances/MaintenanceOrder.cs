using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Maintenances
{
    public class MaintenanceOrder : Entity<int>, ICreationAudited, ISoftDelete
    {
        public int MaintenanceDepartmentId { get; set; }
        /// <summary>
        /// 维修部门
        /// </summary>
        [ForeignKey("MaintenanceDepartmentId")]
        public virtual Department MaintenanceDepartment { get; set; }

        public long? MaintenanceEngineerId { get; set; }
        /// <summary>
        /// 维修工程师
        /// </summary>
        [ForeignKey("MaintenanceEngineerId")]
        public virtual User MaintenanceEngineer { get; set; }
        /// <summary>
        /// 维修备注
        /// </summary>
        public string MaintenanceRemarks { get; set; }

        public long? CallerId { get; set; }
        /// <summary>
        /// 报修人
        /// </summary>
        [ForeignKey("CallerId")]
        public User Caller { get; set; }

        public int CallerDepartmentId { get; set; }
        /// <summary>
        /// 报修部门
        /// </summary>
        [ForeignKey("CallerDepartmentId")]
        public Department CallerDepartment { get; set; }
        /// <summary>
        /// 故障
        /// </summary>
        public string Trouble { get; set; }
        /// <summary>
        /// 客服备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? CompletionTime { get; set; }
        public MaintenanceOrderStatus Status { get; set; }
        public long? RecorderId { get; set; }
        /// <summary>
        /// 客服记录(完成)人
        /// </summary>
        [ForeignKey("RecorderId")]
        public virtual User Recorder { get; set; }
        public long? CreatorUserId { get; set; }
        /// <summary>
        /// 客服派单人
        /// </summary>
        [ForeignKey("CreatorUserId")]
        public virtual User Creator { get; set; }
        public DateTime CreationTime { get; set; }
        public bool IsDeleted { get; set; }
        public float ArrivalSpeed { get; set; }
        /// <summary>
        /// 维修效率
        /// </summary>
        public float RepairEfficiency { get; set; }
        /// <summary>
        /// 服务态度
        /// </summary>
        public float Service { get; set; }
        /// <summary>
        /// 工作质量
        /// </summary>
        public float Quality { get; set; }
       
        public virtual ICollection<MaintenanceTrace> MaintenanceTraces { get; set; }
    }

    public enum MaintenanceOrderStatus
    {
        派单分配=0,
        特别关注=3,
        工单完成=10,
        扫码完成=11
    
    }
}
