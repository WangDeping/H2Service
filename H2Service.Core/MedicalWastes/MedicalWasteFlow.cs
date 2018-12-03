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
    public class MedicalWasteFlow : Entity, ISoftDelete, ICreationAudited
    {
        public MedicalWasteStatus Status { get; set; }

        public int DepartmentId { get; set; }
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }

        /// <summary>
        /// 交接护士
        /// </summary>
        public long? NurseId { get; set; }
        [ForeignKey("NurseId")]
        public virtual User Nurse { get; set; }

        public long? CollectUserId { get; set; }
        [ForeignKey("CollectUserId")]
        public virtual User CollectUser { get; set; }
        public DateTime? CollectTime { get; set; }
        public int? MedicalWasteDeliveryId { get; set; }
        [ForeignKey("MedicalWasteDeliveryId")]
        public virtual MedicalWasteDelivery Delivery { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<MedicalWaste> MedicalWasteCollection { get; set; }
        public virtual ICollection<MedicalWasteImage> ImagesCollection { get; set; }
        public long? CreatorUserId { get; set ; }
        public DateTime CreationTime { get ; set ; }
    }
}
