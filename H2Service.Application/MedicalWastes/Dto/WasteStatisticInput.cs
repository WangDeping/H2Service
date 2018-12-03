using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
 public   class WasteStatisticInput
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int DepartmentId { get; set; }

        public MedicalWasteStatus Status { get; set; }
    }
}
