using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.SMS.Dto
{
    public class SMSSendInput
    {
        public List<string> Mobiles { get; set; }

        public string Content { get; set; }

        public DateTime? Timer{ get; set; }
    }
}
