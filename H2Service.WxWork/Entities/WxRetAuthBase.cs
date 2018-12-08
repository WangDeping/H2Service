using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    public class WxRetAuthBase : WxRetBase
    {
        public string UserId{get;set;}

        public string DeviceId { get; set; }

        public string user_ticket { get; set; }

        public int expires_in { get; set; }
    }
}
