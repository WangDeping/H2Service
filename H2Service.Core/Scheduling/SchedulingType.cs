using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling
{
    /// <summary>
    /// 班次类型
    /// </summary>
    public class SchedulingType : Entity
    {
        /// <summary>
        /// 班次类型名称
        /// </summary>
        public string SchedulingTypeName{get;set;}
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary>
        /// 是否有需要时间限制
        /// </summary>
        public bool IsTimeLimit { get; set; }
        /// <summary>
        ///班次开始时间点， 如果IsTimeLimit为True，此字段不能为空
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 班次结束时间点,如果IsTimeLimit为True，此字段不能为空
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 班次跨天类型
        /// </summary>
        public SpanDayType EndTimeDayType { get; set; }
        /// <summary>
        /// 等效价值(例白班即为1，上午为0.5)
        /// </summary>
        public float SchedulingTypeValue { get; set; }
        /// <summary>
        /// 是否需要考勤
        /// </summary>
        public bool IsRequiredAttence { get; set; }
        /// <summary>
        /// 是否属于听/值班
        /// </summary>
        public bool IsDuty { get; set; }
    }
    /// <summary>
    /// 跨天(T0(T+0)即为当天)
    /// </summary>
    public enum SpanDayType {
        T0,
        T1,
        T2,
        T3,
        T4,
        T5,
        T6

    }
}
