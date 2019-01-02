using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Maintenances.Dto
{
    /// <summary>
    /// input
    /// </summary>
    [AutoMap(typeof(MaintenanceOrder))]
    public   class DispatchMaintenanceOrderInput
    {      
        /// <summary>
        /// 报修部门Id
        /// </summary>
        public int CallerDepartmentId { get; set; }
        /// <summary>
        /// 报修人Id
        /// </summary>
        public long? CallerId { get; set; }
        /// <summary>
        /// 维修部门Id
        /// </summary>
        public int MaintenanceDepartmentId { get; set; }
        /// <summary>
        /// 维修工程师Id
        /// </summary>
        public long? MaintenanceEngineerId { get; set; }
        /// <summary>
        /// 故障
        /// </summary>
        public string Trouble { get; set; }
    }
}
