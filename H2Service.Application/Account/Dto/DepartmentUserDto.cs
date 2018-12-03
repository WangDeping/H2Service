using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
    public class DepartmentUserDto:UserDto
    {
        public long UserId { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
       
    }
}
