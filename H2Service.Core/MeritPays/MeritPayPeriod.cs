using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays
{
    public class MeritPayPeriod : Entity, ICreationAudited
    {
        public string Period { get; set; }

        public long? CreatorUserId { get ; set ; }
        public DateTime CreationTime { get; set; }

        [ForeignKey("CreatorUserId")]
        public virtual User Creator { get; set; }

        public virtual ICollection<MeritPayDetail> DetailCollection { get; set; }
      
    }
}
