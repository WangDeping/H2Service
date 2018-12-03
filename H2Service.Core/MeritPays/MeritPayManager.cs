using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Castle.Core.Logging;
using H2Service.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays
{
 public   class MeritPayManager:DomainService
    {
        private readonly IRepository<MeritPayPeriod> _meritPayPeriodRepository;
        private readonly IRepository<MeritPayDetail> _meritPayDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEventBus _eventsBus;
        private readonly ILogger _logger;
        public MeritPayManager(IRepository<MeritPayPeriod> meritPayPeriodRepository,
             IRepository<MeritPayDetail> meritPayDetailRepository,
              IUnitOfWorkManager unitOfWorkManager,
              IEventBus eventBus,
              ILogger logger)
        {
            _meritPayDetailRepository = meritPayDetailRepository;
            _meritPayPeriodRepository = meritPayPeriodRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _logger = logger;
            _eventsBus = eventBus;
        }

        public void AcceptMeritPay(string period,List<MeritPayDetail> details)
        {          
            var instance = _meritPayPeriodRepository.Insert(new MeritPayPeriod {  Period=period});
            var needConversion = Helper.UserNumberConversionHelper.UserNumberConversionDictionary();             
            foreach (var detail in details)
            {
                var kvUserNumber = needConversion.Where(T => T.Key == detail.UserNumber).FirstOrDefault();
                if (!default(KeyValuePair<string, string>).Equals(kvUserNumber))
                    detail.UserNumber = kvUserNumber.Value;
                var kvHeaderNumber = needConversion.Where(T => T.Key == detail.HeaderNumber).FirstOrDefault();
                if (!default(KeyValuePair<string, string>).Equals(kvHeaderNumber))
                    detail.HeaderNumber = kvHeaderNumber.Value;
                detail.MeritPayPeriodId = instance.Id;                
                _meritPayDetailRepository.Insert(detail);
            }
            _unitOfWorkManager.Current.SaveChanges();
            _eventsBus.Trigger(new MeritPayCreateEventData { Period = instance, UserNumberList = details.Select(d => d.UserNumber).Distinct() });
        }
    }
}
