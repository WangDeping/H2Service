using Abp.Domain.Entities;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    public class ServerRoom : Entity<int>, ISoftDelete
    {
        [Required]
        public string RoomName{get;set;}
        [Required]
        public string Location { get; set; }
        public int? DepartmentId { get; set; }   
        [ForeignKey("DepartmentId")]
        public virtual Department Department { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual IEnumerable<ServerEquipment> ServerEquipments { get; set; }
    }
}
