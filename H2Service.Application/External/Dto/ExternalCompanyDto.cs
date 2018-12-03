using Abp.AutoMapper;
using H2Service.MedicalWastes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.External.Dto
{
    [AutoMap(typeof(ExternalCompany))]
    public   class ExternalCompanyDto
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string Aspect { get; set; }
    }
}
