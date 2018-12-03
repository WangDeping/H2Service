using Abp.AutoMapper;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
    [AutoMap(typeof(Department))]
    public   class DepartmentDto
    {
        public DepartmentDto()
        {
            IsLeaf = false;
        }

        public int Id { get; set; }
        public string DepartmentName { get; set; }
       
        public int FatherId { get; set; }
       
        public int Order { get; set; }

        public DistrictDto District { get; set; }

        public bool IsAllowedHasUser { get; set; }
        public bool IsLeaf { get; set; }
        public string DepartmentPhone { get; set; }
    }
}
