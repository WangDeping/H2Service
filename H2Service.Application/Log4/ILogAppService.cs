using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Log4
{
  public  interface ILogAppService: IApplicationService
    {
        void LogError(string errorInfo);

        void LogWarn(string warnInfo);

       
    }
}
