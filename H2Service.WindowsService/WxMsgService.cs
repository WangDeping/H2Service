using H2Service.DHCQuery;
using H2Service.WxWork.api;
using H2Service.WxWork.WxModel;
using H2Service.WxWork.WxModel.Msg;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

using System.Threading.Tasks;
using System.Timers;

namespace H2Service.WindowsService
{
    partial class WxMsgService : ServiceBase
    {
        public WxMsgService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
              SendMsgToPresident();          
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
        }
        
        public void SendMsgToPresident() {
            try
            {
                Timer timer = new Timer(1000*60*60); //一小时
                timer.Elapsed += new ElapsedEventHandler(ExecutionCode);
                timer.AutoReset = true;
                timer.Enabled = true;              
               
            }
            catch (Exception ex) {
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
                    var textmsg = new WxSendTextMsg() {
                        agentid = "0",
                        touser = "lansecheng|1960|01",
                         text=new WxTextMsgContent() {  content= new InPatient().GetInPatientCount() }
                    };                
            
                    msgApi.SendMsg(textmsg);
                }
            }
            catch (Exception ex) {
               
            }
         }
    }
}
