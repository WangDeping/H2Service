using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class PagedResultWithSumDto<T>:PagedResultDto<T>
    {
        public int SumQty { get; set; }
    }
}
