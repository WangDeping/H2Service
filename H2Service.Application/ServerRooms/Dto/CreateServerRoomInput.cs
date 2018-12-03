using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerRoom))]
    public   class CreateServerRoomInput
    {
        public int Id { get; set; }
        [Required]
        public string RoomName { get; set; }
        [Required]
        public string Location { get; set; }
        public string Description { get; set; }
       
    }
}
