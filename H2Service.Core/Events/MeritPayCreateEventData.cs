using Abp.Events.Bus;
using H2Service.MeritPays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events
{
 public   class MeritPayCreateEventData: EventData
    {
        public MeritPayPeriod Period { get; set; }

        public IEnumerable<string> UserNumberList { get; set; }
    }
}
