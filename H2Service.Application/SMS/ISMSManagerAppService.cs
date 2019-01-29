using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.SMS
{
 public  interface ISMSManagerAppService: IApplicationService
    {
        void Test();
    }
}
