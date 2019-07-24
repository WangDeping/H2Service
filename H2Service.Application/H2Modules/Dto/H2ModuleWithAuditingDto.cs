using Abp.AutoMapper;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.H2Modules
{
    [AutoMap(typeof(H2ModuleWithAuditing))]
    public   class H2ModuleWithAuditingDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public H2Module Module { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        public int? DepartmentId { get; set; }      
        public string DepartmentName { get; set; }
        /// <summary>
        ///初步审核人员
        /// </summary>
        public long? DoUserId { get; set; }      
        public string DoUserName { get; set; }
      /// <summary>
      /// 审核人员
      /// </summary>
        public long? AuditorId { get; set; }
    
        public string AuditorName { get; set; }
     
    }
}
