using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace H2Service.Authorization.Dto
{
 public   class AssignPermissionInput
    {
        public AssignPermissionInput()
        {
            PermissionNames = new List<string>();

        }
        public List<string> PermissionNames { get; set; }

        public long? UserId { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }
    }
}