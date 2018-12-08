using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.DailyInPatients.Dto;
using H2Service.WxWork;
using H2Service.WxWork.Dto;
using H2Service.WxWork.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace H2Service.Hangfire.Jobs.DailyInPatients
{
    public class DailyInPatientsJob : HangfireJobBase<DailyInPatientsJobArgs>
    {
        private readonly WxSender _wxSender;
        public DailyInPatientsJob(WxSender wxSender)
        {
            wxSender = _wxSender;
            
        }
        public override void ExecuteJob(DailyInPatientsJobArgs aParams)
        {          
            
            var msg = new WxSendTextMsg("你好啊", "lansecheng");
            _wxSender.SendMsg(msg);
        }


    }
}
