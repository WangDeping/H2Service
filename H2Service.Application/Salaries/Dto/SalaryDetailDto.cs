using Abp.AutoMapper;
using H2Service.Salarires;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Salaries.Dto
{
 [AutoMap(typeof(SalaryDetail))]
 public   class SalaryDetailDto
    {
        public int Id { get; set; }
        public string UserNumber { get; set; }

        public int SalaryPeriodID { get; set; }
        
        [Required]
        public string Detail { get; set; }

        public string  Period { get; set; }

        public string SalaryTypeName { get; set; }
        
        public string CreatorUser { get; set; }
        public string CreationTime { get; set; }
    }
}
