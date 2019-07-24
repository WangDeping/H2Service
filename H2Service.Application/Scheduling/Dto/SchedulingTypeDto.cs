using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling.Dto
{
    [AutoMap(typeof(SchedulingType))]
    public    class SchedulingTypeDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 班次命名
        /// </summary>
        public string SchedulingTypeName { get; set; }
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }      
        /// <summary>
        ///班次开始时间点
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 班次结束时间点
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 班次跨天类型
        /// </summary>
        public string TimeSpanEnum { get; set; }
        /// <summary>
        /// 等效价值(例白班即为1，上午为0.5)
        /// </summary>
        public decimal SchedulingTypeValue { get; set; }
        /// <summary>
        /// 是否需要考勤
        /// </summary>
        public bool IsRequiredAttence { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public int SchedulingGroupId { get; set; }

    }
}
