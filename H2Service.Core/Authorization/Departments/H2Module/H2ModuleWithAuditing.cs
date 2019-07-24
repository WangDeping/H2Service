using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Departments
{
    /// <summary>
    /// 模块与科室用户审核关系
    /// </summary>
 public   class H2ModuleWithAuditing : Entity, ISoftDelete
    {  
     public H2Module Module { get; set; }
    public int? DepartmentId { get; set; }
    [ForeignKey("DepartmentId")]
    public virtual Department Department { get; set; }
    /// <summary>
    /// 操作人员
    /// </summary>
    public long? DoUserId { get; set; }
    [ForeignKey("DoUserId")]
    public virtual User DoUser { get; set; }
    /// <summary>
    /// 审核人员
    /// </summary>
    public long? AuditorId { get; set; }
    [ForeignKey("AuditorId")]
    public virtual User Auditor { get; set; }
    public bool IsDeleted { get; set; }
}
}
