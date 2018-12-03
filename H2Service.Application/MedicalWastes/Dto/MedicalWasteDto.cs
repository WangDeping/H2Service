using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
    [AutoMap(typeof(MedicalWaste))]
    public   class MedicalWasteDto
    {
        public int Id { get; set; }

        public string CreatorUserName { get; set; }

        public DateTime CreationTime { get; set; }

        public string Code { get; set; }

        public decimal Weight { get; set; }

        public int FlowId { get; set; }

        public long? CreatorUserId { get; set; }

        public MedicalWasteKind Kind { get; set; }
    }
}
