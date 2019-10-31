using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentProperty))]
  public  class EquipmentPropertyDto
    {
        public int Id { get; set; }
        public string EquipmentPropertyName { get; set; }
        public string PartrolType { get; set; }
        public string Frequency { get; set; }//巡视频次  
        public string ViewType { get; set; }
       
    }
}
