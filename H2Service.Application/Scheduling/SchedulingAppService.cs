using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling
{
 public   class SchedulingAppService:H2ServiceAppServiceBase,ISchedulingAppService
    {
        private readonly IRepository<SchedulingGroup> _schedulingGroupRepository;
        private readonly IRepository<SchedulingType> _schedulingTypeRepository;
        private readonly IRepository<SchedulingRecord> _schedulingRecordRepository;

        public SchedulingAppService(IRepository<SchedulingGroup> schedulingGroupRepository,
            IRepository<SchedulingType> schedulingTypeRepository,
            IRepository<SchedulingRecord> schedulingRecordRepository) {
            _schedulingGroupRepository = schedulingGroupRepository;
            _schedulingRecordRepository = schedulingRecordRepository;
            _schedulingTypeRepository = schedulingTypeRepository;
        }


        public void GetSchedulingTypeGroup() {


        }

    }
}
