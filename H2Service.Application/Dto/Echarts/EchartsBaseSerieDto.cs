using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class EchartsBaseSerieDto
    {
        public EchartsBaseSerieDto()
        {
            data = new List<decimal>();
        }
        public string name { get; set; }

        public string type { get; set; }

        public List<decimal> data { get; set; }
    }
}
