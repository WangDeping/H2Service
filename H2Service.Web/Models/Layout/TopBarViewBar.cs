using H2Service.Account.Dto;
using H2Service.Users.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Layout
{
    public class TopBarViewBar
    {
        public UserDto User { get; set; }
        public DepartmentDto Department {get;set;}
       
    }
}