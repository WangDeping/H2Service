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
            var wxUser = new WxCreateUserInfo
            {
                avatar_mediaid = eventData.AvatarUrl,
                department =new int[]{ eventData.DepartmentId },
                gender = eventData.Gender,
                mobile = eventData.TelPhone,
                name = eventData.UserName,
                userid = eventData.UserNumber
            };

            _wxUserManager.Create(wxUser);
        }
    }
}
