using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
 public   class EndUsageInput
    {
        public int Id { get; set; }

        public DateTime EndTime { get; set; }

        public long EndUserId { get; set; }
    }
}
