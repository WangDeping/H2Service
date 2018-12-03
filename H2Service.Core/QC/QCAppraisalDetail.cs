using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2Service.QC
{
 public   class QCAppraisalDetail: Entity,ICreationAudited
    {
        public int DepartmentPunishmentPeriodId { get; set; }
        [ForeignKey("DepartmentPunishmentPeriodId")]
        public virtual QCAppraisalPeriod DepartmentPunishmentPeriod { get; set; }

        /// <summary>
        /// 职能科室
        /// </summary>
        public int FunctionalDepartmentId { get; set; }
        [ForeignKey("FunctionalDepartmentId")]
        public virtual Department FunctionalDepartment { get; set; }

        /// <summary>
        /// 受罚科室
        /// </summary>
        public int PunishedDepartmentId { get; set; }
        [ForeignKey("PunishedDepartmentId")]
        public virtual Department PunishedDepartment { get; set; }

        /// <summary>
        /// 受罚人
        /// </summary>
        public long? PunishedUserId { get; set; }
        [ForeignKey("PunishedUserId")]
        public virtual User PunishedUser { get; set; }

        public string Desc { get; set; }

       public decimal Points { get; set; }

        public long? CreatorUserId { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }

        public DateTime CreationTime { get ; set; }
        public bool IsDeleted { get ; set; }
    }
}
