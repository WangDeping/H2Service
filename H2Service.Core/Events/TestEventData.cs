using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events
{
 public   class TestEventData:EventData
    {
        public string Name { get; set; }
    }
}
