using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using H2Service.Dto;
namespace H2Service.MedicalWastes.Dto
{
 public   class GetHandOverFlowListInput
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int DepartmentId { get; set; }
    }
}
