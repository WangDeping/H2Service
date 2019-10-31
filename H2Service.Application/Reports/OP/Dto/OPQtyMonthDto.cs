using Abp.AutoMapper;
using H2Service.MedicalData.OPMedicalDiagnose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Reports.Dto
{
    [AutoMap(typeof(OPMedicalDiagnose))]
    public   class OPQtyMonthDto
    {
        public string Month { get; set; }

        public string Dep { get; set; }
    }
}
