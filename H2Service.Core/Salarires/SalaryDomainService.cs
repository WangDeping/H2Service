using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Castle.Core.Logging;
using H2Service.Events;
using H2Service.WeChatWork;
using System.Collections.Generic;
using System.Linq;

namespace H2Service.Salarires
{
    public    class SalaryDomainService:DomainService,ISalaryDomainService
    {
        private readonly IRepository<SalaryPeriod> _salaryPeriodTypeRepository;
        private readonly IRepository<SalaryDetail> _salaryDetailRepository;
        //private readonly IWxWorkService _wxWrokService;
        private readonly IEventBus _eventsBus;
        private readonly ILogger _logger;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public SalaryDomainService(IRepository<SalaryPeriod> salaryPeriodRepository,
            IRepository<SalaryDetail> salaryDetailRepository,
            ILogger logger,
            IEventBus eventBus,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _salaryPeriodTypeRepository = salaryPeriodRepository;
            _salaryDetailRepository = salaryDetailRepository;          
            _eventsBus = eventBus;
            _logger =logger;
            _unitOfWorkManager = unitOfWorkManager;
        }
        
        /// <summary>
        /// 保存工资区间，并且保存该工资区间内所有人明细
        /// </summary>
        /// <param name="salaryperiod"></param>
        /// <param name="detailsList"></param>
        public void AcceptSalaryDetail(SalaryPeriod salaryperiod,IEnumerable<SalaryDetail> detailsList) {

            var needConversion = Helper.UserNumberConversionHelper.UserNumberConversionDictionary();
            foreach (var detail in detailsList)
            {
                var kvPair = needConversion.Where(T => T.Key == detail.UserNumber).FirstOrDefault();
                if (!default(KeyValuePair<string,string>).Equals(kvPair))
                    detail.UserNumber = kvPair.Value;
                detail.SalaryPeriodID = salaryperiod.Id;
                _salaryDetailRepository.Insert(detail);                
            }
            _unitOfWorkManager.Current.SaveChanges();
           _eventsBus.Trigger(new SalaryCreateEventData {  Period=salaryperiod, UserNumberList=detailsList.Select(d=>d.UserNumber)});
          //  _logger.Error("事件触发后，期数"+salaryperiod.Period+"工号"+ string.Join("|", detailsList.Select(d => d.UserNumber).ToArray()));

        }
     
     }
}
