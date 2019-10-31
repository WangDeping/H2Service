using H2Service.Hangfire.Framework;
using H2Service.SMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Hangfire.Jobs.MinutelyPingHost
{
    public class MinutelyPingHostJob : HangfireJobBase<NoneJobParam>
    {
        private readonly ISMSManagerAppService _smsAppService;
        public MinutelyPingHostJob(ISMSManagerAppService smsAppService) {
            _smsAppService = smsAppService;
        }

        public override void ExecuteJob(NoneJobParam aParams)
        {
           var sms= new SMS.Dto.SMSSendInput
            {
                Content = "PING不通",
                Mobiles = new List<string> { "18678621306" }
            };

            var host = "192.168.6.166";
            var  ping = new Ping();
            var reply = ping.Send(host);
            if (reply.Status == IPStatus.Success) {
                sms.Content = "网络正常";
            }
            _smsAppService.Send(sms);
        }
    }
}
