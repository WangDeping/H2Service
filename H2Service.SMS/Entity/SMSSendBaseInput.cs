using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.SMS.Entity
{
 internal   class SMSSendBaseInput:SMSEntityBase
    {
        public SMSSendBaseInput() {
            mobiles = new List<string>();           
        }
        internal IList<string> mobiles { get; set; }

        internal string content { get; set; }

        internal DateTime? timerTime { get; set; }

        internal string customSmsId { get; set; }    

    }
}
