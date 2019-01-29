using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling
{
 public   class SchedulingGroup:Entity,ISoftDelete
    {
        /// <summary>
        /// 班次类型分组
        /// </summary>
        public string SchedulingGroupName { get; set; }
        /// <summary>
        /// 班次分组下的班次类型集合
        /// </summary>
        public virtual ICollection<SchedulingType> SchedulingTypes { get; set; }
        public bool IsDeleted { get; set ; }
    }
}
