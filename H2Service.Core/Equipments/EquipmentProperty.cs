using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    /// <summary>
    /// 巡视属性
    /// </summary>
 public   class EquipmentProperty:Entity
    {
        public string EquipmentPropertyName { get; set; }
        public PatrolTypeEnum PartrolType { get; set; }
        public EquipmentPartrolFrequencyEnum Frequency { get; set; }//巡视频次  
        public ViewType ViewType { get; set; }
        public virtual ICollection<EquipmentType> EquipmentTypes { get; set; }

    }

    public enum EquipmentPartrolFrequencyEnum {
        每日=1,
        每周=2,
        每月=3,
        每季度=4,
        每半年=5,
        每年=6
    }
    public enum ViewType {
        checkbox=1,
        text=2,
        select3=3
    }
   
}
