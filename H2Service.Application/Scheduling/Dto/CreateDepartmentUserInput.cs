using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling.Dto
{
 public   class CreateDepartmentUserInput
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        public int DepartmentId { get; set; }
        /// <summary>
        /// 若有重复强制性新建
        /// </summary>
        public bool Mandatory { get; set; }
    }
}
