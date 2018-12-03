using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
 public   class PagedSortedAndFilteredInputDto: PagedAndSortedInputDto
    {
        public string Filter { get; set; }
         //接收DataTables的参数
         public int Draw { get; set; }
         public int Length
         {
             get
             {
                 return this.MaxResultCount;
             }
 
             set
             {
                 this.MaxResultCount = value;
             }
         }
         public int Start
         {
             get
             {
                 return this.SkipCount;
             }
 
             set
             {
                 this.SkipCount = value;
             }
         }
    }
}
