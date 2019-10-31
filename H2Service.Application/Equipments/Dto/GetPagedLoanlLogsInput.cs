using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
 public   class GetPagedLoanlLogsInput : PagedInputDto
    {
        public int DepartmentId { get; set; }
    }
}
