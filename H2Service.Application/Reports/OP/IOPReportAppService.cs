using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.Reports.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Reports
{
 public   interface IOPReportAppService: IApplicationService
    {
        PagedResultWithSumDto<GetOutPatientsQtyByDepOutput> GetOutPatientsQty(GetOutPatientsQtyByDepInput input);

        List<GetOutPatientsInPeriodOutput> GetOutPatientsInPeriod(GetOutPatientsInPeriodInput input);
    }
}
