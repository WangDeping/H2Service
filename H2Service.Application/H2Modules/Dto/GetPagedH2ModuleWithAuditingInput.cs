using H2Service.Authorization.Departments;
using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.H2Modules.Dto
{
 public   class GetPagedH2ModuleWithAuditingInput: PagedInputDto
    {
        public H2Module Module { get; set; }

        public string DepartmentName { get; set; }
    }
}
