using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class DataTablesPagedOutputDto<T>: PagedResultDto<T>
    {
        public int Draw { get; set; }
 
         /// <summary>
         /// 过滤后的记录数（没有就是全部），这个是必须的参数
         /// </summary>
         public int RecordsFiltered { get; set; }
 
         public int RecordsTotal { get { return this.TotalCount; } }
       
         public DataTablesPagedOutputDto(int totalCount, IReadOnlyList<T> items)
           : base(totalCount, items)
         {
             this.RecordsFiltered = totalCount;
         }
}
}
