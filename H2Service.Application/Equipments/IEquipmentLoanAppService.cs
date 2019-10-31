using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public  interface IEquipmentLoanAppService: IApplicationService
    {
        void   LoanEqupment(LoanEquipmentInput input);

        EquipmentLoanLogDto GetLoanLog(int Id);

        void DoneLoanLog(DoneLoanLogInput input);

        PagedResultDto<EquipmentLoanLogDto> GetPagedLoanLogs(GetPagedLoanlLogsInput input);
    }
}
