using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.H2Module
{
    public class SetModulePermissionModel
    {
        public int Id { get; set; }
     
        public int Module { get; set; }
        /// <summary>
        /// 科室Id
        /// </summary>
        public int? DepartmentId { get; set; }
      
        /// <summary>
        ///初步审核人员
        /// </summary>
        public long? DoUserId { get; set; }
      
        /// <summary>
        /// 审核人员
        /// </summary>
        public long? AuditorId { get; set; }

       public string PermissionName { get; set; }
    }
}