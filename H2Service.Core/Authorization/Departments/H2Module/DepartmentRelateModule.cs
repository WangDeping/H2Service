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
        医疗废物=0,
        考勤排班=1,
        质量控制1=2,//职能科室
        质量控制2=3,//被督查科室
        机房科室=4,
        需排班科室=5,
        设备管理=6,
        护理病区=7
    }

}