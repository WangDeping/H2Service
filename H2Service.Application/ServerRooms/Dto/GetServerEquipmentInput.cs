using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
 public   class GetServerEquipmentInput: PagedInputDto
    {
        public string IP { get; set; }

        public string SEName { get; set; }   

        public string SEType { get; set; }

        public string OperatingSystem { get; set; }
    }
}
