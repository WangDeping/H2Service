using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Maintenances.Dto
{
    [AutoMap(typeof(MaintenanceOrder))]
    public   class GetPagedMaintenanceOrdersOutput
    {
        public int MaintenanceDepartmentId { get; set; }
        /// <summary>
        /// 维修部门
        /// </summary>        
        public string MaintenanceDepartmentName { get; set; }

        public long? MaintenanceEngineerId { get; set; }
        /// <summary>
        /// 维修工程师
        /// </summary>        
        public string MaintenanceEngineerName { get; set; }
        /// <summary>
        /// 维修备注
        /// </summary>
        public string MaintenanceRemarks { get; set; }
        /// <summary>
        /// 上报人
        /// </summary>
        public long? CallerId { get; set; }
        /// <summary>
        /// 报修人
        /// </summary>
        
        public string  CallerName { get; set; }

        public int CallerDepartmentId { get; set; }
        /// <summary>
        /// 报修部门
        /// </summary>      
        public string CallerDepartmentName { get; set; }
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
        /// <summary>
        /// 维修单状态
        /// </summary>
        public MaintenanceOrderStatus Status { get; set; }        
        /// <summary>
        /// 派单人
        /// </summary>
        public long? CreatorUserId { get; set; }
        /// <summary>
        /// 客服派单人
        /// </summary>       
        public string CreatorName { get; set; }
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 综合得分
        /// </summary>
        public float Grade { get; set; }
       
    }
}
