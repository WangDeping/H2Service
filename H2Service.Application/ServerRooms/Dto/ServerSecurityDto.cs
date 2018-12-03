using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerEquipment))]
    public   class ServerSecurityDto
    {
        [Required]
        public int Id { get; set; }

        //登录用账户
        [Required]
        public string Account { get; set; }

        //登录用密码
        [Required]
        public string Password { get; set; }

        public bool IsMonitored { get; set; }
    }
}
