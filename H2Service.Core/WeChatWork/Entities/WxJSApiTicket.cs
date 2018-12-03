using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork.Entities
{
 public   class WxJSApiTicket: WxRetMsgBase
    {
        public string ticket { get; set; }

        public int expires_in { get; set; }
    }
}
