using Abp.Domain.Entities;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    public class EquipmentPatrolLog : Entity
    {
        /// <summary>
        /// 设备
        /// </summary>
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }
        public PatrolTypeEnum Type { get; set; }

        public virtual ICollection<EquipmentPatrolDetail> PatrolDetails { get; set; }
        public bool MainCheck { get; set; }
        public string Summary { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
    }

    public enum PatrolTypeEnum {
        不区分=0,
        I级巡视=1,
        II级巡视=2
       
    }
}
