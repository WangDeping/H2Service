using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class PagedAndSortedInputDto: PagedInputDto, ISortedResultRequest
    {
        public string Sorting { get; set; }
 
         public PagedAndSortedInputDto()
         {
             MaxResultCount = H2ServiceConsts.DefaultPageSize;
         }
    }
}
