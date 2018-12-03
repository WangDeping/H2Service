using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.External.Dto
{
  public  class GetExternalUsersInput: PagedInputDto
    {
        public int ExternalCompanyId { get; set; }
    }
}
