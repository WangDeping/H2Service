using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using H2Service.WeChatWork;
using H2Service.WeChatWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events.Handler
{
    public class CreateUserEventHandler : IEventHandler<CreateUserEventData>, ITransientDependency
    {
        private readonly WxUserManager _wxUserManager;
        private readonly ILogger _logger;
        public CreateUserEventHandler(WxUserManager wxUserManager,
            ILogger logger)
        {
            _wxUserManager = wxUserManager;
            _logger = logger;
        }
        public void HandleEvent(CreateUserEventData eventData)
        {
            var wxUser = new WxUserInfo
            {
                avatar_mediaid = eventData.AvatarUrl,
                department = eventData.DepartmentId,
                gender = eventData.Gender.ToString(),
                mobile = eventData.TelPhone,
                name = eventData.UserName,
                userid = eventData.UserNumber
            };

            _wxUserManager.Create(wxUser);
        }
    }
}
