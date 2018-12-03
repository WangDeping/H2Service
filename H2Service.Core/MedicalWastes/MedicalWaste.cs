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
    /// <summary>
    /// 最小包装医疗废物
    /// </summary>
    public class MedicalWaste : Entity, ICreationAudited,ISoftDelete
    {
        public MedicalWasteKind Kind { get; set; }
      
        public decimal Weight { get; set; }
        public string Code { get; set; }
        public long? CreatorUserId { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }
        public DateTime CreationTime { get ; set; }

        [ForeignKey("FlowId")]
        public virtual MedicalWasteFlow Flow { get; set; }
        public int FlowId { get; set; }
        public bool IsDeleted { get; set ; }
       
    }
    public enum MedicalWasteKind {
        感染性废物,
        病理性废物,
        损伤性废物,
        化学性废物,
        药学性废物
    }

    public enum MedicalWasteStatus
    {
        科室点=0,
        医院暂存点=1,
        卫康公司=2

    }
}
