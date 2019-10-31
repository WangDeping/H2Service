using Abp.Runtime.Caching;
using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.DailyOPDiagnoseSynchronous.Dto;
using H2Service.Log4;
using H2Service.OPDiagnose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.Hangfire.Jobs.DailyOPDiagnoseSynchronous
{
    public class DailyOPDiagnoseSynchronousJob : HangfireJobBase<DailyOPDiagnoseSynchronousJobArgs>
    {
        private readonly IOPDiagnoseAppService _OPDiagnoseAppService;
        private readonly ICacheManager _cacheManager;
        private ILogAppService _logAppService;
        public DailyOPDiagnoseSynchronousJob(IOPDiagnoseAppService OPDiagnoseAppService,
             ICacheManager cacheManager, ILogAppService logAppService) {
            _OPDiagnoseAppService = OPDiagnoseAppService;
            _cacheManager = cacheManager;
            _logAppService = logAppService;
        }


        public override void ExecuteJob(DailyOPDiagnoseSynchronousJobArgs aParams)
        {
            //开始时间从web.cofig存入到redis中,以后都从redis中获取
            /*var dateFrom = DateTime.Parse(_cacheManager.GetCache("SynchronousDate").Get("OPDiagnose", () =>
            {
                return WebConfigurationManager.AppSettings["OPDiagnoseSynchronousJobDateFrom"];
            }));*/
            var dateFrom = DateTime.Now.AddDays(-1).Date.ToString("yyyy-MM-dd");
            /*var dateTo = DateTime.Parse(WebConfigurationManager.AppSettings["OPDiagnoseSynchronousJobDateTo"]);
             if (dateFrom <= dateTo)
             {
                 //受限制每次只能查询一天
                 var strDateFrom = dateFrom.ToString("yyyy-MM-dd");
                 _OPDiagnoseAppService.SynchronousDiagnose(strDateFrom, strDateFrom);
                 _logAppService.LogError(strDateFrom + "门诊诊断同步完成");
                 _cacheManager.GetCache("SynchronousDate").Set("OPDiagnose", dateFrom.AddDays(1).ToString("yyyy-MM-dd"));
             }*/
            _OPDiagnoseAppService.SynchronousDiagnose(dateFrom, dateFrom);
            _logAppService.LogError(dateFrom + "门诊诊断同步完成");
        }
    }
}
