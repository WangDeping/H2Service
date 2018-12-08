using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
 public   class WxJSApiTicket: WxRetBase
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }
    }
}
