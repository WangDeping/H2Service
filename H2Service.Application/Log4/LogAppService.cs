using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Log4
{
    public class LogAppService : H2ServiceAppServiceBase, ILogAppService
    {
        public void LogError(string errorInfo)
        {
            Logger.Error(errorInfo);
        }

        public void LogWarn(string warnInfo)
        {
            Logger.Warn(warnInfo);
        }
    }
}
