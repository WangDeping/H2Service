using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
    [AutoMap(typeof(MedicalWaste))]
    public   class AppendWasteInput
    {
        
        public int FlowId { get; set; }

        public MedicalWasteKind Kind { get; set; }

        public decimal Weight { get; set; }
        
    }
}
