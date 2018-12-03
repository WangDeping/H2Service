using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class ErrMsgDto<T> where T:class
    {
        public int ErrId { get; set; }

        public string ErrMsg { get; set; }

        public T RetDto { get; set; }
    }
}
