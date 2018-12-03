using H2Service.Account.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Helpers
{
    public class DepartmentsListEquality : IEqualityComparer<DepartmentDto>
    {
        public bool Equals(DepartmentDto x, DepartmentDto y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(DepartmentDto obj)
        {
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return obj.ToString().GetHashCode();
            }
        }
    }
}