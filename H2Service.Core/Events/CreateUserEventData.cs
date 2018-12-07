using Abp.Events.Bus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events
{
 public   class CreateUserEventData:EventData
    {
        public string UserNumber { get; set; }

        public string UserName { get; set; }

        public string TelPhone { get; set; }

        public int DepartmentId { get; set; }//remote微信端部门Id

        public int Gender { get; set; }

        public string AvatarUrl { get; set; }
    }
}
