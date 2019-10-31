using H2Service.SMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2Service.SMS.Dto;
using Abp.UI;

namespace H2Service.SMS
{
 public   class SMSManagerAppService : H2ServiceAppServiceBase, ISMSManagerAppService
    {
        private SMSSimpleInter _smsSimperInter;
        public SMSManagerAppService(SMSSimpleInter smsSimperInter) {
            _smsSimperInter = smsSimperInter;

        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="input">手机号和短信内容</param>
        public void Send(SMSSendInput input)
        {
            if (input.Mobiles.Count == 0||string.IsNullOrEmpty(input.Content))
                throw new UserFriendlyException("手机号和内容不得为空");            
            _smsSimperInter.Send(input.Mobiles,input.Content );
        }
        /// <summary>
        /// 获取余额
        /// </summary>
        /// <returns></returns>
        public string GetBalance() { 
           return _smsSimperInter.GetBalance();
           // _smsSimperInter.GetMo();           
        }
    }
}
