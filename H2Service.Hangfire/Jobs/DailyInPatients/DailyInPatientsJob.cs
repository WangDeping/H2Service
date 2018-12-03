using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.DailyInPatients.Dto;
using H2Service.WeChatWork;
using H2Service.WeChatWork.Dto;
using H2Service.WeChatWork.Entities;
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
        private IWxAppService _wxAppService;
        public DailyInPatientsJob(IWxAppService wxAppService)
        {
            _wxAppService = wxAppService;
            
        }
        public override void ExecuteJob(DailyInPatientsJobArgs aParams)
        {            
            var msg = new WxSendTextMsgDto()
            {
                agentid = WebConfigurationManager.AppSettings["assistantAppid"],               
                touser = "lansecheng"
            };
            msg.text = new WxTextMsgContent { content = "你好啊" };
            _wxAppService.SendMsg(msg);
        }


    }
}
