using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.External.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.External
{
 public   interface IExternalCompanyAppService:IApplicationService
    {
        PagedResultDto<ExternalCompanyDto> GetPagedExternalCompanies(PagedInputDto input);

        void CreateExternalCompany(ExternalCompanyDto input);

        void UpdateExternalCompany(ExternalCompanyDto input);

        ExternalCompanyDto GetExternalCompany(int Id);

    }
}
