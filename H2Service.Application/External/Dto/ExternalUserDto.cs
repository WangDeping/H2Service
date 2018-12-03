using Abp.AutoMapper;
using H2Service.Authorization;
using H2Service.MedicalWastes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.External.Dto
{
    [AutoMap(typeof(ExternalUser))]
    public   class ExternalUserDto
    {
        public int Id { get; set; }

        public string ExternalCompanyId { get; set; }

        public string UserName { get; set; }
        
        //public string Code { get; set; }

        public string TelPhone { get; set; }

        public string IDNumber { get; set; }

       // public string AvatarUrl { get; set; }

        public string Gender { get; set; }

        public string ExternalCompanyName { get; set; }
    }
}
