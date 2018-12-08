using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    public class WxSendMsg
    {
        public string touser { get; set; }

        public string toparty { get; set; }

        public string totag { get; set; }

        public string msgtype { get; set; }

        public string agentid { get; set; }

        public string safe { get; set; }

        public WxSendMsg()
        {
            this.safe = "0";

            this.agentid = "0";//默认是企业小助手
        }
    }
}
