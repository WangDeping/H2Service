using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
    /// <summary>
    /// 医疗垃圾出库
    /// </summary>
    public class MedicalWasteDelivery : Entity, ISoftDelete, ICreationAudited
    {

        public virtual ICollection<MedicalWasteFlow> MedicalWasteFlowCollection { get; set; }
        public int DistrictId { get; set; }
        /// <summary>
        /// 医疗垃圾收集点位置(即东西院区)
        /// </summary>
        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        public string Summary { get; set; }
        public bool IsDeleted { get; set; }
        public long? CreatorUserId { get ; set ; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }
        public DateTime CreationTime { get ; set ; }
    }
}
