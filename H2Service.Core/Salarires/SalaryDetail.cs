using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Salarires
{
 public   class SalaryDetail:Entity
    {
        public string UserNumber { get; set; }

        public int SalaryPeriodID { get; set; }

        public virtual SalaryPeriod SalaryPeriod { get; set; }
        [Required]
        public string Detail { get; set; }

       
    }
}
