using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public   class EquipmentKind:Entity
    {
        public string EquipmentKindName { get; set; }

        public virtual ICollection<Equipment> Equipments { get; set; }
    }
}
