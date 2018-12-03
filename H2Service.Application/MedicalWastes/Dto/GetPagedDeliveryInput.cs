using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
 public   class GetPagedDeliveryInput: PagedInputDto
    {
        public int DistrictId { get; set; }
    }
}
