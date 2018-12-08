using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.WxWork.Dto
{
 public   class WxUserInfoDetailListDto: RetDto
    {
        public List<WxUserInfoDetailDto> userlist { get; set; }
    }
}
