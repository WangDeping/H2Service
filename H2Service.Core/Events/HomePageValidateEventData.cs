using Abp.Events.Bus;
using H2Service.MedicalData.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events
{
    public class HomePageValidateEventData : EventData
    {     
        public bool Validate { get; set; }
        public HomePageValidateMessage ValidateMessage{ get; set; }
    }
}
