
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
 public   class WxGetUserBaseInfo:WxRetBase
    {
        public string userid { get; set; }

        public string name { get; set; }

        public string mobile { get; set; }

        public string avatar { get; set; }

        public string gender { get; set; }
    }
}
