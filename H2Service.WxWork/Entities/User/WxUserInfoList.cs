using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
 public   class WxUserInfoList:WxRetBase
    {
        public List<WxGetUserInfo> userlist { get; set; }
    }
}
