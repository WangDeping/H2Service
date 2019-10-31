using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
   
    [AutoMap(typeof(EquipmentPatrolDetail))]
    public   class EquipmentPatrolDetailDto
    {
        public int Id { get; set; }

        public int PatrolId { get; set; }       
        
        public int PropertyId { get; set; }
       
        public string PropertyName { get; set; }


        public string Result { get; set; }
    }
    
}
