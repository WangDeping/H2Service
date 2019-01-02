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
    public class SchedulingRecord : Entity, ISoftDelete, ICreationAudited
    {
        /// <summary>
        /// 班次日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        
        public long? CreatorUserId { get ; set; }
        [ForeignKey("CreatorUserId")]
        public User CreatorUser { get; set; }
        public DateTime CreationTime { get; set; }

        public bool IsChecked { get; set; }
        public bool IsDeleted { get; set; }
    }
}
