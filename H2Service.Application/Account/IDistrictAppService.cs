using Abp.Application.Services;
using H2Service.Account.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account
{
 public  interface IDistrictAppService:IApplicationService
    {
        IEnumerable<DistrictDto> GetDistrictList();
    }
}
