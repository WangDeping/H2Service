using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
  public  class GetPatrolsInput
    {

        public   int RoomId { get; set; }

        public DateTime? DateStart { get; set; }

        public DateTime? DateEnd { get; set; }

       
    }
}
