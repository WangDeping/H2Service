using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
    [AutoMap(typeof(MedicalWaste))]
    public    class GroupWasteDto
    {      
        public int Id { get; set; }

        public int  Kind { get; set; }

        public decimal TotalWeight { get; set; }
    
        public long? CreatorUserId { get; set; }
        
        public int Count { get; set; }

        public string KindName { get; set; }

        public string CreatorUserName { get; set; }
        public DateTime CreationTime { get; set; }
      
    }
}
