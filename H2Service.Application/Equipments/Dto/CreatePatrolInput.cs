using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentPatrolLog))]
    public   class CreatePatrolInput
    {
        public CreatePatrolInput() {
            PatrolDetails = new List<EquipmentPatrolDetailDto>();
        }
        public  int EquipmentId { get; set; }

        public PatrolTypeEnum Type { get; set; }
        public long? CreatorUserId { get; set; }
        public IList<EquipmentPatrolDetailDto> PatrolDetails { get; set; }
        public string Summary { get; set; }
        public bool MainCheck { get; set; }
    }
}
