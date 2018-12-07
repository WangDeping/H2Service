using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Users
{
    public class ExternalUserModel
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string UserName { get; set; }

        public string Code { get; set; }

        public string TelPhone { get; set; }

        public string UserNumber { get; set; }

        public string AvatarUrl { get; set; }

        public int Gender { get; set; }

        public int RemoteDepartmentId { get; set; }
    }
}