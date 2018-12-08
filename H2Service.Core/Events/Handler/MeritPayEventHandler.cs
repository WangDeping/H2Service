using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.Events.Handler
{
    public class MeritPayEventHandler : IEventHandler<MeritPayCreateEventData>, ITransientDependency
    {
        private readonly WxSender _wxSender;
        private readonly ILogger _logger;
        public MeritPayEventHandler(WxSender wxSender,
            ILogger logger)
        {
            _wxSender = wxSender;
            _logger = logger;
        }
        public void HandleEvent(MeritPayCreateEventData eventData)
        {
            string tousers = string.Join("|", eventData.UserNumberList.ToArray());    
            var newsMsg = new WxSendNewsMsg(
                 eventData.Period.Period + "个人绩效公示",
            WebConfigurationManager.AppSettings["meritPayWxNotifPic"],
            "绩效公示",
             string.Format(WebConfigurationManager.AppSettings["meritPayWxNotifUrl"], eventData.Period.Id),
             tousers
               );
            var retMsg = _wxSender.SendMsg(newsMsg);
            _logger.Error("绩效通知失效人员:" + retMsg.invaliduser);
        }
    }
}
