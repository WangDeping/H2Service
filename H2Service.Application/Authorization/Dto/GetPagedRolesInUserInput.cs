using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Dto
{
 public   class GetPagedRolesInUserInput:PagedInputDto
    {
        public long UserId { get; set; }
    }
}
