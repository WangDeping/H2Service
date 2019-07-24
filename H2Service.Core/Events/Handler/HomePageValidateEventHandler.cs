using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Events.Bus.Handlers;
using H2Service.MedicalData.HomePages;
using H2Service.WxWork;
using H2Service.WxWork.Entities;
using H2Service.WxWork.Entities.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.Events.Handler
{
    public class HomePageValidateEventHandler : IEventHandler<HomePageValidateEventData>, ITransientDependency
    {
        private WxSender _wxSender;
        private IRepository<HomePageValidateMessage> _validateMessageRepository;

        public HomePageValidateEventHandler(WxSender wxSender,
            IRepository<HomePageValidateMessage> validateMessageRepository) {
            _wxSender = wxSender;
            _validateMessageRepository =validateMessageRepository;
        }

        public void HandleEvent(HomePageValidateEventData eventData)
        {
            var message = eventData.ValidateMessage;
            var existMsg = _validateMessageRepository.FirstOrDefault(T => T.AdmNo == message.AdmNo);
            if (existMsg != null)
                _validateMessageRepository.Delete(existMsg);
            //校验没通过,发送通知  
            if (!eventData.Validate) {                
                var id=_validateMessageRepository.InsertAndGetId(message);
                if (eventData.ValidateMessage.DischargeDate!=null)
                {
                    var title = string.Format(message.ValidateType.ToString() + "未通过({0})", message.BAH);
                    var url = string.Format(WebConfigurationManager.AppSettings["appBaseUrl"] + @"HomePageValidate/ValidateMessage/{0}", id);
                    var description = string.Format("<div class='highlight'>{0}</div>", eventData.ValidateMessage.Message);
                    var userNumber = eventData.ValidateMessage.UserNumber;
                    var wxMsg = new WxSendTextCardMsg(description, title, url, userNumber, WebConfigurationManager.AppSettings["homepageAppid"]);
                    //var wxMsg = new WxSendTextMsg(description,userNumber);
                    _wxSender.SendMsg(wxMsg);
                }
               
            }   
        }
    }
}
