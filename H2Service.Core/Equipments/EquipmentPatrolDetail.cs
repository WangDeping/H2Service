using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    
    public class EquipmentPatrolDetail : Entity
    {
        
        public int PatrolId { get; set; }
        [ForeignKey("PatrolId")]
        public virtual EquipmentPatrolLog PatrolLog { get; set; }
        public int PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public virtual EquipmentProperty Property { get; set; }
        

        public string Result { get; set; }
        
    }
}
