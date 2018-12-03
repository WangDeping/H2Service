using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
  public  class DisplayWasteInDepartmentOutput
    {
        public List<GroupWasteDto> GroupWasteList { get; set; }

        public List<WasteImageDto> Images { get; set; }

        public List<MedicalWasteDto> DetailWasteList { get; set; }

        public WasteFlowDto Flow { get; set; }

        public int FlowId { get; set; }
    }
}
