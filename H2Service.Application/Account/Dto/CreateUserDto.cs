using Abp.AutoMapper;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Users.Dto
{
    [AutoMap(typeof(User))]
    public   class CreateUserDto
    {
        public string UserName { get; set; }

        public int Id { get; set; }

        public string UserNumber { get; set; }

        public string AvatarUrl { get; set; }

        public string TelPhone { get; set; }

        public string TelPhone2 { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

    }
}
