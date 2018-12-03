using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays.Dto
{
    [AutoMap(typeof(MeritPayPeriod))]
    public   class CreateMeritPayPeriodInput
    {
        public int Id { get; set; }
        public string Period { get; set; }

        public  List<MeritPayDetailDto> DetailCollection { get; set; }
    }
}
