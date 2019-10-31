using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Reports.Dto
{
    /// <summary>
    /// 图表用门诊人数输入参数
    /// </summary>
 public   class GetOutPatientsInPeriodInput
    {
        /// <summary>
        /// 区间类型
        /// </summary>
        public PeriodTypes Type { get; set; }

        public string Dep { get; set; }

    }
    public enum PeriodTypes {
        本年月份=0,
        历史年度=1

    }
}
