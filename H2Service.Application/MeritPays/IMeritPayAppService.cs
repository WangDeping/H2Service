using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.MeritPays.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays
{
 public  interface IMeritPayAppService:IApplicationService
    {
        void UploadMeritPeriod(CreateMeritPayPeriodInput input);

        PagedResultDto<MeritPayPeriodDto> GetPagedSalaryPeriods(PagedInputDto input);

        List<MeritPayDetailDto> HeaderDetails(int periodId, string userNumber);

        void RemovePeriod(int Id);

        List<MeritPayDetailDto> PersonalDetails(int periodId);
    }
}
