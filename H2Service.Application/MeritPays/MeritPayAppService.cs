using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.MeritPays.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MeritPays
{
 public   class MeritPayAppService: H2ServiceAppServiceBase,IMeritPayAppService
    {
        private readonly IRepository<MeritPayPeriod> _meritPayPeriodRepository;
        private readonly IRepository<MeritPayDetail> _meritPayDetailRepository;
        private readonly MeritPayManager _meritPayManager;
        public MeritPayAppService(IRepository<MeritPayPeriod> meritPayPeriodRepository,
             IRepository<MeritPayDetail> meritPayDetailRepository,
             MeritPayManager meritPayManager)
        {
            _meritPayDetailRepository = meritPayDetailRepository;
            _meritPayPeriodRepository = meritPayPeriodRepository;
            _meritPayManager = meritPayManager;
        }

        
        public PagedResultDto<MeritPayPeriodDto> GetPagedSalaryPeriods(PagedInputDto input)
        {
            var query =_meritPayPeriodRepository.GetAll();
            var periodsCount = query.Count();
            var periodList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<MeritPayPeriodDto> { Items = periodList.MapTo<List<MeritPayPeriodDto>>(), TotalCount = periodsCount };
        }
        [AbpAuthorize(PermissionNames.Pages_Salary_MeritPayUpload)]
        public void RemovePeriod(int Id)
        {
            var period = _meritPayPeriodRepository.Get(Id);
            if (period != null)
                _meritPayPeriodRepository.Delete(period);

        }
        /// <summary>
        /// 用于个人查看绩效明细,一个人在一个绩效区间可有多个绩效明细
        /// </summary>
        /// <param name="periodId"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<MeritPayDetailDto> PersonalDetails(int periodId)
        {
            var userNumber = AbpSession.GetUserNumber();           
            var details = _meritPayDetailRepository.GetAllList(T => T.MeritPayPeriodId == periodId&&T.UserNumber==userNumber);
            return details.MapTo<List<MeritPayDetailDto>>();
        }
        /// <summary>
        /// 用于科室主任查看本科室下所有人员绩效明细
        /// </summary>
        /// <param name="periodId">绩效区间</param>
        /// <param name="userNumber">主任工号</param>
        /// <returns></returns>
        [AbpAuthorize]
        public List<MeritPayDetailDto> HeaderDetails(int periodId,string userNumber)
        {         
            var details = _meritPayDetailRepository.GetAllList(T => T.MeritPayPeriodId == periodId && T.HeaderNumber== userNumber);
            return details.MapTo<List<MeritPayDetailDto>>();
        }

        [AbpAuthorize(PermissionNames.Pages_Salary_MeritPayUpload)]
        public void UploadMeritPeriod(CreateMeritPayPeriodInput input)
        {
            if(_meritPayPeriodRepository.FirstOrDefault(T=>T.Period==input.Period)!=null)
                 throw new UserFriendlyException("该期数据已经上传");
            _meritPayManager.AcceptMeritPay(input.Period, input.DetailCollection.MapTo<List<MeritPayDetail>>());
        }


      
    }
}
