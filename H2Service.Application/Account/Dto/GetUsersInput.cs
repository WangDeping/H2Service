using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Users.Dto
{
  public  class GetUsersInput: PagedInputDto
    {
        public string UserNumber { get; set; }

        public string UserName { get; set; }

        public string DepartmentName { get; set; }

        public int DepartmentId { get; set; }//20181019
    }
}
