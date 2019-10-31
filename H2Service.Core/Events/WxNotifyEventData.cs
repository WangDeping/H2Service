using Abp.Events.Bus;
using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Events
{
  public  class WxNotifyEventData: EventData
    {
        public WxSendMsg WxMsg { get; set; }
    }
}
