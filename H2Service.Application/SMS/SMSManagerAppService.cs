using H2Service.SMS.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.SMS
{
 public   class SMSManagerAppService : H2ServiceAppServiceBase, ISMSManagerAppService
    {
        private SMSSimpleInter _smsSimperInter;
        public SMSManagerAppService(SMSSimpleInter smsSimperInter) {
            _smsSimperInter = smsSimperInter;

        }
        public void Test() {         

           // _smsSimperInter.Send(new List<string> { "18678621306","18054624306"},"你好");
            _smsSimperInter.GetBalance();
            _smsSimperInter.GetMo();
            _smsSimperInter.GetReport();
        }
    }
}
