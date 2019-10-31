using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    public class EquipmentType : Entity
    {
        public string EquipmentTypeName { get; set; }     
        
        public string Icon { get; set; }

        public virtual ICollection<EquipmentProperty> EquipmentProperties { get; set; }
    }
}
