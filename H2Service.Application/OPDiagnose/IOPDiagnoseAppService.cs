using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.OPDiagnose
{
 public   interface IOPDiagnoseAppService:IApplicationService
    {
        /// <summary>
        /// 同步门诊诊断
        /// </summary>
        /// <param name="dateFrom">开始日期</param>
        /// <param name="dateTo">结束日期</param>
        void SynchronousDiagnose(string dateFrom, string dateTo);
    }
}
