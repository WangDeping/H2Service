using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork.Entities
{
    public class WxSendNewsMsg : WxSendMsg
    {
        public WxSendNewsMsg()
        {
            this.msgtype = "news";
        }
        public WxNewsMsgArticleCollection news { get; set; }
    }
}
