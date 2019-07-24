using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling
{
 public   class SchedulingDepartmentUser:Entity, ICreationAudited
    {
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        public long? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public long? CreatorUserId { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User Creator { get; set; }
        public DateTime CreationTime { get ; set ; }
    }
}
