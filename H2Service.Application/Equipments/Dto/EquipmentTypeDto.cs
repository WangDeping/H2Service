using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentType))]
    public   class EquipmentTypeDto
    {
        public int Id { get; set; }

        public string EquipmentTypeName { get; set; }
    }
}
