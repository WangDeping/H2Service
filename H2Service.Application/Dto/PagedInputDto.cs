using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class PagedInputDto: IPagedResultRequest
    {
        /// <summary>
          /// 每页显示的行数
          /// </summary>
         [Range(1, H2ServiceConsts.MaxPageSize)]
          public int MaxResultCount { get; set; }
          /// <summary>
         /// 跳过数量=MaxResultCount*页数
         /// </summary>
         
        [Range(0, int.MaxValue)]
        public int PageNumber { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount {
            get => this.PageNumber * this.MaxResultCount;
           set=>throw new NotImplementedException();
    }
        

        public PagedInputDto()
         {
             MaxResultCount = H2ServiceConsts.DefaultPageSize;
        }
}
}
