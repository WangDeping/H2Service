using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Maintenances.Dto
{
 public   class CompleteOrderInput:CompleteOrderBaseInput
    {
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

        /// <summary>
        /// 客服备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
