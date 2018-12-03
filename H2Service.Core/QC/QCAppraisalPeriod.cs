using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.QC
{
  public  class QCAppraisalPeriod:Entity,ICreationAudited,ISoftDelete
    {
        public string Title { get; set; }

        public string Period { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public long? CreatorUserId { get; set ; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }

        public int? CreatorDepartmentId { get; set; }
        [ForeignKey("CreatorDepartmentId")]
        public virtual Department CreatorDepartment { get; set; }
        public DateTime CreationTime { get ; set ; }

        public QCAppraisalPeriodStatus Status { get; set; }
        public virtual ICollection<QCAppraisalDetail> Punishments { get; set; }
        public bool IsDeleted { get ; set ; }
    }

    public enum QCAppraisalPeriodStatus
    {       
        发布,
        审核

    }
}
