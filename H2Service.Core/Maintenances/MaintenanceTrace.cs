using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Maintenances
{
    public class MaintenanceTrace : Entity, ICreationAudited
    {

        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual MaintenanceOrder Order { get; set; }

        public long? CreatorUserId { get ; set; }
        [ForeignKey("CreatorUserId")]
        public User CreatorUser { get; set; }
        public DateTime CreationTime { get; set ; }
    }
}
