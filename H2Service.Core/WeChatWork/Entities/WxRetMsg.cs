﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork.Entities
{
    /// <summary>
    /// 微信主动发送消息返回值
    /// </summary>
 public   class WxRetMsg:WxRetMsgBase
    {
        public string invaliduser { get; set; }

        public string invalidparty { get; set; }

        public string invalidtag { get; set; }
    }
}