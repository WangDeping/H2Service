using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
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
        ///班次开始时间点
        /// </summary>
        public string StartTime { get; set; }
        /// <summary>
        /// 班次结束时间点
        /// </summary>
        public string  EndTime { get; set; }
        /// <summary>
        /// 班次跨天类型
        /// </summary>
        public TimeSpanEnum TimeSpanEnum { get; set; }
        /// <summary>
        /// 等效价值(例白班即为1，上午为0.5)
        /// </summary>
        public  decimal SchedulingTypeValue { get; set; }
        /// <summary>
        /// 是否需要考勤
        /// </summary>
        public bool IsRequiredAttence { get; set; } 

        public int SchedulingGroupId { get; set; }
        [ForeignKey("SchedulingGroupId")]
        public virtual SchedulingGroup SchedulingGroup { get; set; }

    }
    /// <summary>
    /// 跨天(T0(T+0)即为当天)
    /// </summary>
    public enum TimeSpanEnum
    {
        T0,
        T1,
        T2,
        T3
    }
}
