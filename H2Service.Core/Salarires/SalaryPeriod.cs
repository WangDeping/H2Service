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

namespace H2Service.Salarires
{
 public   class SalaryPeriod:Entity<int>,ICreationAudited
    {
       [Required]
        public string Period { get; set; }

        public int SalaryTypeID { get; set; }

        public virtual SalaryType SalaryType { get; set; }
       
        public virtual ICollection<SalaryDetail> SalaryDetails { get; set; }
        public long? CreatorUserId { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }        
       
        public DateTime CreationTime { get; set ; }
       
    }
    
}
