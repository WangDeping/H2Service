using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    /// <summary>
    /// 微信根据code获取用户验证信息,若非企业用户则返回OpenId
    /// </summary>
 public   class WxAuthUserInfo:WxRetBase
    {
        public string UserId { get; set; }

        public string DeviceId { get; set; }

        public string OpenId { get; set; }
    }
}
