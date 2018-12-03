using Abp.Events.Bus;
using H2Service.Salarires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events
{
 public   class SalaryCreateEventData:EventData
    {
        public SalaryPeriod Period { get; set; }

        public IEnumerable<string> UserNumberList { get; set; }
    }
}
