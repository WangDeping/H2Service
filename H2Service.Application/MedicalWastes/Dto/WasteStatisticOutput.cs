using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
 public   class WasteStatisticOutput
    {
        public decimal Total { get; set; }

        public int PackageCount { get; set; }

        public MedicalWasteKind Kind { get; set; }
    }
}
