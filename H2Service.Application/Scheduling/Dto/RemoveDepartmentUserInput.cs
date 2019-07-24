using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling.Dto
{
 public   class RemoveDepartmentUserInput
    {
        public int DepartmentId { get; set; }

        public long? UserId { get; set; }
    }
}
