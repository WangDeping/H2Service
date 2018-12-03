using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WeChatWork.Entities
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
 public   class WxUserInfo
    {
        public string userid { get; set; }
        public string name { get; set; }
        public string mobile { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public string avatar { get; set; }
        public string qr_code { get; set; }
    }
}
