using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    /// <summary>
    /// 微信用户信息
    /// </summary>
 public   class WxCreateUserInfo
    {
        public string userid { get; set; }

        public string name { get; set; }

        public string mobile { get; set; }

        public Array department { get; set; }

        public int gender { get; set; }

        public string email { get; set; }

        public string telephone { get; set; }

        public string avatar_mediaid { get; set; }
    }
}
