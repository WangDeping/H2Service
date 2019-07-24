using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling.Dto
{
    /// <summary>
    /// 分页查询需排班科室
    /// </summary>
 public   class GetAllSchedulingDepartmentsInput: PagedInputDto
    {
        /// <summary>
        /// 科室名称
        /// </summary>
        public string DepartmentName { get; set; }
    }
}
