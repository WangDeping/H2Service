using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{   
    public   class GetHandOverFlowListDto: WasteFlowDto
    {
        public string Infectious { get; set; }
        public string Damage { get; set; }
        public string Pathology { get; set; }
        public string Chemical { get; set; }
        public string Pharmaceutical { get; set; }

        public string District { get; set; }
    }
}
