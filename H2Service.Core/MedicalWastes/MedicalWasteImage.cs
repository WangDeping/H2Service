using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
    public class MedicalWasteImage : Entity, ICreationAudited
    {

        public string ImgPath { get; set; }

        public MedicalWasteStatus Status { get; set; }

        public int FlowId { get; set; }

        [ForeignKey("FlowId")]
        public virtual MedicalWasteFlow Flow { get; set; }

        public long? CreatorUserId { get ; set ; }

        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }

        public DateTime CreationTime { get ; set ; }
    }
}
