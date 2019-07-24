using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Configuration;
namespace H2Service.Events.Handler
{
    public class SalaryEventHandler : IEventHandler<SalaryCreateEventData>, ITransientDependency
    {
        private readonly WxSender _wxSender;
        private readonly ILogger _logger;        
        public SalaryEventHandler(WxSender wxSender,
            ILogger logger) {
           
            _wxSender = wxSender;
            _logger =logger;
            

        }
        public void HandleEvent(SalaryCreateEventData eventData)
        {           
            string tousers=string.Join("|", eventData.UserNumberList.ToArray()) ;  
            var newsMsg = new WxSendNewsMsg(
                eventData.Period.Period + "工资条",
                WebConfigurationManager.AppSettings["salaryWxNotifPic"],
                "发钱啦",
                string.Format(WebConfigurationManager.AppSettings["salaryWxNotifUrl"], eventData.Period.Id),
                tousers
                );            
            var retMsg = _wxSender.SendMsg(newsMsg);
            _logger.Error("工资通知失效人员:" + retMsg.invaliduser);           
        }
    }
}
