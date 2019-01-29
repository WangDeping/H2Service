using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
 public   class WasteTraceInfo
    {
        /// <summary>
        /// 医疗废物分类  
        /// </summary>
        public MedicalWasteKind Kind { get; set; }
        /// <summary>
        /// 重量
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 称重人
        /// </summary>
        public string CreatorUserName { get; set; }
        /// <summary>
        /// 称重时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 生产科室
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 条码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 交接人员
        /// </summary>
        public string NurseName { get; set; }
        /// <summary>
        /// 收集人员
        /// </summary>
        public string CollectUserName { get; set; }
        /// <summary>
        /// 科室交接时间
        /// </summary>
        public string CollectTime { get; set; }
        /// <summary>
        /// 暂存点交接人员
        /// </summary>
        public string DeliveryCreatorName { get; set; }
        /// <summary>
        /// 暂存点交接时间
        /// </summary>
        public string DeliveryCreationTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public MedicalWasteStatus Status { set; get; }
    }
}
