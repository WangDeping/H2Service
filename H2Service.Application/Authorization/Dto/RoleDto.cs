using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Dto
{
    [AutoMap(typeof(Role))]
    public   class RoleDto
    {
        public int Id { get; set; }

        public string RoleName { get; set; }
    }
}
