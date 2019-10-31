using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Events.Bus;
using H2Service.Events;
using H2Service.WxWork;
using H2Service.WxWork.Entities.Msg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.MedicalData.HomePages
{
 public   class HomePageValidateDomainService: DomainService
    {
        private readonly IRepository<HomePageValidateMessage> _validateMessageRepository;
        private readonly IEventBus _eventBus;
        private WxSender _wxSender;
        private string _validateUrl = "";
        private string _homePageAppId = "";
        public HomePageValidateDomainService(IRepository<HomePageValidateMessage> validateMessageRepository,
            IEventBus eventBus,
            WxSender wxSender) {
            _validateMessageRepository = validateMessageRepository;
            _wxSender = wxSender;
            _eventBus = eventBus;
            _validateUrl = WebConfigurationManager.AppSettings["appBaseUrl"] + @"HomePageValidate/ValidateMessage/{0}";
            _homePageAppId = WebConfigurationManager.AppSettings["homepageAppid"];
        }
        /// <summary>
        /// 住院总审核不通过发消息
        /// </summary>
        /// <param name="message"></param>
        public void SendQCValidateMessage(HomePageValidateMessage message) {
            var Id=  _validateMessageRepository.InsertAndGetId(message);
            {
                var title = string.Format("住院总审核未通过({0})", message.BAH);
                var url = string.Format(_validateUrl, Id);
                var description = string.Format("<div class='highlight'>{0}</div>", message.Message);
                var userNumber = message.UserNumber;
                var wxMsg = new WxSendTextCardMsg(description, title, url, userNumber, _homePageAppId);

                _eventBus.Trigger(new WxNotifyEventData{ WxMsg = wxMsg });
            }
           
        }
        /// <summary>
        /// 完善病历后通知住院总
        /// </summary>
        /// <param name="Id">通知Id</param>
        public void CorrectThenNotify(int Id) {
            var msg = _validateMessageRepository.FirstOrDefault(T=>T.Id==Id);
            msg.SendTime = DateTime.Now;
            msg.ValidateType = ValidateType.临床改正反馈;            
            {
                var title = string.Format("住院总:病历({0})已经修正请再审核", msg.BAH);
                var url = string.Format(_validateUrl, Id);
                var description = string.Format("<div class='highlight'>{0}</div>", msg.Message);
                var userNumber = msg.SendUser;//住院总接收
                var wxMsg = new WxSendTextCardMsg(description, title, url, userNumber, _homePageAppId);
                _eventBus.Trigger(new WxNotifyEventData { WxMsg = wxMsg });
            }
        }
        /// <summary>
        /// 住院总审核通过问题病历
        /// </summary>
        /// <param name="Id">消息Id</param>
        public void PassValidate(int Id) {
            var msg = _validateMessageRepository.FirstOrDefault(T => T.Id == Id);
            msg.ValidateStatus = ValidateStatus.完成;
            msg.Message = new StringBuilder(msg.Message).AppendLine("住院总审核通过时间:" + DateTime.Now.ToString()).ToString();
            msg.IsDeleted = true;
            Logger.Error("审核通过了信息"+Id);
        }
        /// <summary>
        /// 住院总审核未通过，回退
        /// </summary>
        /// <param name="Id">消息Id</param>
        public void NoPassValidate(int Id) {
            var msg = _validateMessageRepository.FirstOrDefault(T => T.Id == Id);
            msg.ValidateType= ValidateType.住院总质控;
            msg.ValidateStatus = ValidateStatus.问题通知;
            msg.SendTime = DateTime.Now;
            {
                var title = string.Format("回退:病历({0})被住院总审核回退", msg.BAH);
                var url = string.Format(_validateUrl, Id);
                var description = string.Format("<div class='highlight'>{0}</div>", msg.Message);
                var userNumber = msg.UserNumber;//临床医生总接收
                var wxMsg = new WxSendTextCardMsg(description, title, url, userNumber, _homePageAppId);
                _eventBus.Trigger(new WxNotifyEventData { WxMsg = wxMsg });

            }
        }

        public void FileHomePage(string bah) {
            var pages = _validateMessageRepository.GetAll().Where(T => T.BAH == bah);
            foreach (var page in pages) {
                page.IsDeleted = true;
                page.ValidateStatus = ValidateStatus.病案上架;
                _validateMessageRepository.Update(page);
            }
        }
        public void DeleteMessage(int Id) {
            Logger.Error("删除了信息"+Id);
            _validateMessageRepository.Delete(Id);
        }

        public IEnumerable<HomePageValidateMessage> GetAllMessage() {
            return _validateMessageRepository.GetAll();
        }
        public HomePageValidateMessage GetMessageById(int Id) {

            return _validateMessageRepository.FirstOrDefault(T => T.Id == Id);
        }
    }

}
