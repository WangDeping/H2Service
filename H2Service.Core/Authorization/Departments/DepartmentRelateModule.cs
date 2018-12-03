using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2Service.Authorization.Departments
{
    public class DepartmentRelateModule : Entity<int>, ISoftDelete
    {
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId ")]
        public virtual Department Department { get; set; }
        public H2Module Module { get; set; }
        public bool IsDeleted { get ; set ; }
    }
    public enum H2Module {
        医疗废物,
        考勤排班,
        质量控制1,//职能科室
        质量控制2,//被督查科室
        机房科室
    }

}