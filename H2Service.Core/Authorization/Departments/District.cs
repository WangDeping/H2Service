using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Departments
{
    public class District : Entity<int>, ISoftDelete
    {
        public string DistrictName { get; set; }

        public bool IsDeleted { get ; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
