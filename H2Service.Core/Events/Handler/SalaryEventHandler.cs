using Abp.Dependency;
using Abp.Events.Bus.Handlers;
using Castle.Core.Logging;
using H2Service.WeChatWork;
using H2Service.WeChatWork.Entities;
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
            string userNumber =string.Join("|", eventData.UserNumberList.ToArray()) ;          
            //封装图文微信消息
            var newsArticles = new List<WxNewsMsgArticle>() { new WxNewsMsgArticle {
             description =eventData.Period.Period+"工资条",
             picurl=WebConfigurationManager.AppSettings["salaryWxNotifPic"],
             title="发钱啦",
             url=string.Format(WebConfigurationManager.AppSettings["salaryWxNotifUrl"], eventData.Period.Id)
            }
            };           
         
            var  wxArticles = new WxNewsMsgArticleCollection();
            wxArticles.articles = newsArticles;
            var retMsg = _wxSender.SendMsg(new WxSendNewsMsg {
                 touser=userNumber,
                 news=wxArticles
            });
            _logger.Error("工资通知失效人员:" + retMsg.invaliduser);           
        }
    }
}
