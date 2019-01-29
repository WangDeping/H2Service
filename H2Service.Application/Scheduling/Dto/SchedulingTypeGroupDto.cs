using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling.Dto
{
    [AutoMap(typeof(SchedulingGroup))]
    public   class SchedulingTypeGroupDto
    {
        public int Id { get; set; }
       
        public string SchedulingGroupName { get; set; }
    }
}
