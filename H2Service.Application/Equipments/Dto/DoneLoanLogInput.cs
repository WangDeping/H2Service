using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
 public   class DoneLoanLogInput
    {
        public int Id { get; set; }

        public long ReturnUserId { get; set; }

        public long SignUserId { get; set; }

        public DateTime ReturnTime { get { return DateTime.Now; } }
    }
}
