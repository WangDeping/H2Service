using Abp.AutoMapper;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
    [AutoMap(typeof(District))]
    public   class DistrictDto
    {
        public int Id { get; set; }

        public string DistrictName { get; set; }

    }
}
