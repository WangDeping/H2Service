using H2Service.WxWork.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Entities
{
    /// <summary>
    /// 永久图片上传后的返回
    /// </summary>
  public  class WxRetImg:WxRetBase
    {
        public string url { get; set; }
    }
}
