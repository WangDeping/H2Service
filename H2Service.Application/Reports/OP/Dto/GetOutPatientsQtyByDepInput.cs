using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Reports.Dto
{
 public   class GetOutPatientsQtyByDepInput: PagedInputDto
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public string Dep { get; set; }
    }
}
