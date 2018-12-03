using Abp.AutoMapper;
using H2Service.Salarires;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace H2Service.Salaries.Dto
{
    [AutoMap(typeof(SalaryPeriod))]
    public   class CreateSalaryPeriodInput
    {
        [Required]
        public String Period { get; set; }

        [Required]
        public int SalaryTypeID { get; set; }

        
        public virtual IList<SalaryDetailDto> SalaryDetailList { get; set; }

        
        public long? CreatorUserId { get; set; }
    }
}
