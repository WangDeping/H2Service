using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
 public   class GetEquipmentInput: PagedInputDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }
        public List<int> DepIds { get; set; }

        
    }
}
