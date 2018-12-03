using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays
{
 public   class MeritPayDetail: Entity
    {
        public string UserNumber { get; set; }

        public string HeaderNumber { get; set; }
      
        public int MeritPayPeriodId { get; set; }

        [ForeignKey("MeritPayPeriodId")]
        public virtual MeritPayPeriod MeritPayPeriod { get; set; }

        public string Detail { get; set; }
    }
}
