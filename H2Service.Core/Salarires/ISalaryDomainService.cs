using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Salarires
{
 public   interface ISalaryDomainService:IDomainService
    {
         void AcceptSalaryDetail(SalaryPeriod salaryperiod, IEnumerable<SalaryDetail> detailsList);
    }
}
