using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    /// <summary>
    /// 临时素材上传返回
    /// </summary>
 public   class WxRetTempFile:WxRetBase
    {
        public string type { get; set; }

        public string media_id { get; set; }

        public string created_at { get; set; }
    }
}
