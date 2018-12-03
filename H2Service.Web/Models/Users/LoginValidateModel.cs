using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Users
{
    public class LoginValidateModel
    {
        public string UserNumber { get; set; }
        public string ValidateCode { get; set; }
    }
}