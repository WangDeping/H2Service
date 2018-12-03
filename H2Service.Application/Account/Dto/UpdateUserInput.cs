using Abp.AutoMapper;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
    [AutoMap(typeof(User))]
    public   class UpdateUserInput
    {
        public string UserName { get; set; }

        public int Id { get; set; }       
       

        public string TelPhone { get; set; }       

        public int Gender { get; set; }
        public string Email { get; set; }
       
    }
}
