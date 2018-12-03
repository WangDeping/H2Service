using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class PagedAndFilteredInputDto: IPagedResultRequest
    {
         [Range(1, H2ServiceConsts.MaxPageSize)]
         public int MaxResultCount { get; set; }
 
         [Range(0, int.MaxValue)]
         public int SkipCount { get; set; }
 
         public string Filter { get; set; }
 
         public PagedAndFilteredInputDto()
         {
             MaxResultCount = H2ServiceConsts.DefaultPageSize;
         }
}
}
