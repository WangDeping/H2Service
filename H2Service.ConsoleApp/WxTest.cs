using H2Service.DHCQuery;
using H2Service.WxWork.api;
using H2Service.WxWork.WxModel;
using H2Service.WxWork.WxModel.Msg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace H2Service.ConsoleApp
{
 public   class WxTest
    {
        public void SendMsgToPresident()
        {
            try
            {
                Timer timer = new Timer(1000*10); //间隔10秒
                timer.Elapsed += new ElapsedEventHandler(ExecutionCode);
                timer.AutoReset =false;
                timer.Enabled = true;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void ExecutionCode(object source, ElapsedEventArgs e)
        {
            try
            {
                if (DateTime.Now.Hour == 17)
                {
                    WxSendMsgApi msgApi = new WxSendMsgApi();
                    var textmsg = new WxSendTextMsg()
                    {
                        agentid = "0",
                        touser = "lansecheng|1960",
                        text = new WxTextMsgContent() { content = new InPatient().GetInPatientCount() }
                    };

                    msgApi.SendMsg(textmsg);
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
