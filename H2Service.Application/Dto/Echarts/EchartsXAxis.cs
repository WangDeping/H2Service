using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class EchartsXAxis
    {
        public EchartsXAxis() {
            data = new List<string>();

        }
       public List<string> data { get; set; }
    }
}
