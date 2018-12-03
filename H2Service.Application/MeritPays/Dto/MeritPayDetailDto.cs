using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays.Dto
{
    [AutoMap(typeof(MeritPayDetail))]
    public   class MeritPayDetailDto
    {
        public int Id { get; set; }

        public string UserNumber { get; set; }

        public string HeaderNumber { get; set; }

        public int MeritPayPeriodId { get; set; }
     

        public string Detail { get; set; }
    }
}
