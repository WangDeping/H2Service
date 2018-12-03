using Abp.AutoMapper;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Dto
{
    [AutoMap(typeof(User))]
    public   class SignInInput: GetUserByNumberOutput
    {       
     
    }
}
