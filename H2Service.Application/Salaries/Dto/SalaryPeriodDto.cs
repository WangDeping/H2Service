using H2Service.Authorization;
using H2Service.Salarires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Salaries.Dto
{
 public   class SalaryPeriodDto
    {
        public int Id { get; set; }
        public string Period { get; set; }

        public int SalaryTypeID { get; set; }

       
        public string SalaryTypeName { get; set; }
       
        public string UserName { get; set; }
        
       public string CreationTime { get; set; }

        public IList<SalaryDetailDto> SalaryDetailList { get; set; }
    }
}
