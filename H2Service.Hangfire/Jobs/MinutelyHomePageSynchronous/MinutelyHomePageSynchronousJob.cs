using Abp.Runtime.Caching;
using H2Service.Hangfire.Framework;
using H2Service.Hangfire.Jobs.MinutelyHomePageSynchronous.Dto;
using H2Service.Log4;
using H2Service.MedicalData.HomePages;
using System;
using System.Web.Configuration;

namespace H2Service.Hangfire.Jobs.MonthlyHomePageSynchronous
{
    public class MinutelyHomePageSynchronousJob : HangfireJobBase<MinutelyHomePageSynchronousJobArgs>
    {
        private readonly HomePageDomainService _homePageDomainService;
        private readonly ICacheManager _cacheManager;
        private ILogAppService _logAppService;
        public MinutelyHomePageSynchronousJob(HomePageDomainService homePageDomainService,
            ICacheManager cacheManager,
            ILogAppService logAppServcie) {
            _homePageDomainService = homePageDomainService;
            _cacheManager = cacheManager;
            _logAppService = logAppServcie;
        }
        public override void ExecuteJob(MinutelyHomePageSynchronousJobArgs aParams)
        {
            //开始时间从web.cofig存入到redis中,以后都从redis中获取
            var dateFrom = DateTime.Parse(_cacheManager.GetCache("SynchronousDate").Get("HomePage", () =>
                {
                    return WebConfigurationManager.AppSettings["HomePageSynchronousJobDateFrom"];
                }));

            var dateTo=DateTime.Parse(WebConfigurationManager.AppSettings["HomePageSynchronousJobDateTo"]);        
            if (dateFrom <=dateTo)
            {
                //受限制每次只能查询一天
                var strDateFrom = dateFrom.ToString("yyyy-MM-dd");              
                _homePageDomainService.SynchronousHomePage(strDateFrom, strDateFrom);
                _logAppService.LogError(strDateFrom+"病案首页同步完成");
                _cacheManager.GetCache("SynchronousDate").Set("HomePage", dateFrom.AddDays(1).ToString("yyyy-MM-dd"));                
            }
        }
    }
}
