using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerRoom))]
    public   class ServerRoomDto
    {
        public int Id { get; set; }
       
        public string RoomName { get; set; }
       
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string Location { get; set; }
        public string Description { get; set; }
    }
}
