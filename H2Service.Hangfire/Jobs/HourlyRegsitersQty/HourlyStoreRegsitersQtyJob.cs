using Abp.Runtime.Caching;
using H2Service.Hangfire.Framework;
using H2Service.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Hangfire.Jobs.HourlyRegsitersQty
{
    public class HourlyStoreRegsitersQtyJob : HangfireJobBase<NoneJobParam>
    {
        private readonly PatientsReportAppService _patientsReportAppService;
        private readonly ICacheManager _cacheManager;
        public HourlyStoreRegsitersQtyJob(PatientsReportAppService patientsReportAppService,
           ICacheManager cacheManager) {
            _patientsReportAppService = patientsReportAppService;
            _cacheManager = cacheManager;
        }
        public override void ExecuteJob(NoneJobParam aParams)
        {
            var currentHour = DateTime.Now.Hour;
            if (currentHour == 0) {
                _cacheManager.GetCache("RegistersQty").Clear();
            }
            var endDate = DateTime.Now.Date.AddHours(currentHour);
            var startDate = endDate.AddHours(-1);
            _patientsReportAppService.StoreOutPatientsQty(startDate,endDate);
        }
    }
}
