using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork.Entities
{
    public class WxSendTextMsg : WxSendMsg
    {
        public WxTextMsgContent text { get; set; }
        public WxSendTextMsg()
        {
            this.msgtype = "text";
        }
    }
}
