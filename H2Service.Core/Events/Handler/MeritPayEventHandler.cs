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
            string userNumber = string.Join("|", eventData.UserNumberList.ToArray());
            //封装图文微信消息
            var newsArticles = new List<WxNewsMsgArticle>() { new WxNewsMsgArticle {
             description =eventData.Period.Period+"个人绩效公示",
             picurl=WebConfigurationManager.AppSettings["meritPayWxNotifPic"],
             title="绩效公示",
             url=string.Format(WebConfigurationManager.AppSettings["meritPayWxNotifUrl"], eventData.Period.Id)
            }
            };

            var wxArticles = new WxNewsMsgArticleCollection();
            wxArticles.articles = newsArticles;
            var retMsg = _wxSender.SendMsg(new WxSendNewsMsg
            {
                touser = userNumber,
                news = wxArticles
            });
            _logger.Error("绩效通知失效人员:" + retMsg.invaliduser);
        }
    }
}
