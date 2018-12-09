using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    public class WxSendTextMsg : WxSendMsg
    {
        [JsonProperty]
        internal WxTextMsgContent text { get; set; }
        private WxSendTextMsg()
        {
          
        }

        public WxSendTextMsg(string content,string touser)
        {
            this.msgtype = "text";
            text = new WxTextMsgContent();
            text.content = content;
            this.touser = touser;
        }
    }
}
