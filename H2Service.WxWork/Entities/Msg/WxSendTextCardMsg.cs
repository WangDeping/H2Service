using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities.Msg
{
 public   class WxSendTextCardMsg: WxSendMsg
    {
        [JsonProperty]
        private WxTextCardMsg textcard { get; set; }
        private WxSendTextCardMsg()
        {

        }

        public WxSendTextCardMsg(string description, string title, string url, string touser, string agentid = "0")
        {
            this.touser = touser;
            this.agentid = agentid;
            this.msgtype = "textcard";
            this.textcard = new WxTextCardMsg()
            {
                title = title,
                description = description,
                url = url,
                btntxt = "查看详情"
            };

        }
    }
}
