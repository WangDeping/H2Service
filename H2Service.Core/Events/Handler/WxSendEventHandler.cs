using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events.Handler
{
    public class WxSendEventHandler : IEventHandler<WxNotifyEventData>, ITransientDependency
    {
        private readonly WxSender _wxSender;
        public WxSendEventHandler(WxSender wxSender) {
            _wxSender =wxSender;
        }
        public void HandleEvent(WxNotifyEventData eventData)
        {
            _wxSender.SendMsg(eventData.WxMsg);
        }
    }
}
