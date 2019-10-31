using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
 public   class GetPatrolLogsInput : PagedInputDto
    {
        public int? DepartmentId { get; set; }

        public PatrolTypeEnum Type { get; set; }

        public string Code { get; set; }

        public DateTime Begin { get; set; }

        public DateTime End { get; set; }
    }
}
