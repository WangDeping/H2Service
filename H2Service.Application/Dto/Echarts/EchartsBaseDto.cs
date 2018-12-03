using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
  public  class EchartsBaseDto
    {
        public EchartsBaseDto() {

            XAxis = new EchartsXAxis();
            this.YAxis = new List<decimal>();
            this.Series = new List<EchartsBaseSerieDto>();
        }
        public EchartsXAxis  XAxis{ get; set; }

        public List<decimal> YAxis { get; set; }

        public List<EchartsBaseSerieDto> Series { get; set; }
    }
}
