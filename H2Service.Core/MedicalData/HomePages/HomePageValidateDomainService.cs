using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Abp.Events.Bus;
using H2Service.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.HomePages
{
 public   class HomePageValidateDomainService: DomainService
    {
        private readonly IRepository<HomePageValidateMessage> _validateMessageRepository;
        private readonly IEventBus _eventBus;
        public HomePageValidateDomainService(IRepository<HomePageValidateMessage> validateMessageRepository,
            IEventBus eventBus) {
            _validateMessageRepository = validateMessageRepository;
           
        }
     
    }

}
