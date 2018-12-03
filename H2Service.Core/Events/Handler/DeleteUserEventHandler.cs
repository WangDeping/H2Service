using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using H2Service.WeChatWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events.Handler
{
    public class DeleteUserEventHandler : IEventHandler<DeleteUserEventData>, ITransientDependency
    {
        private readonly WxUserManager _wxUserManager;
        public DeleteUserEventHandler(WxUserManager wxUserManager)
        {
            _wxUserManager = wxUserManager;
        }

        public void HandleEvent(DeleteUserEventData eventData)
        {
            _wxUserManager.Delete(eventData.UserNumber);
        }
    }
}
