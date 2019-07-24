using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Dto;
using H2Service.Extensions;
using H2Service.MedicalData.HomePages;
using H2Service.QC.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.QC
{
public    class QCAppService: H2ServiceAppServiceBase,IQCAppService
    {
        private readonly IRepository<QCAppraisalPeriod> _qCAppraisalPeriodRepository;
        private readonly IRepository<QCAppraisalDetail> _qCAppraisalDetailRepository;
        private readonly HomePageDomainService _homePageDomianService;

        public QCAppService(IRepository<QCAppraisalPeriod> qCAppraisalPeriodRepository,
            IRepository<QCAppraisalDetail> qCAppraisalDetailRepository,
           HomePageDomainService homePageDomianService)
        {
            _qCAppraisalPeriodRepository = qCAppraisalPeriodRepository;
            _qCAppraisalDetailRepository = qCAppraisalDetailRepository;
            _homePageDomianService = homePageDomianService;
        }
        public PagedResultDto<QCAppraisalPeriodDto> GetPagedQCAppraisalPeriod(PagedInputDto input)
        {
            var query = _qCAppraisalPeriodRepository.GetAll();
            var count = query.Count();
            var list = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<QCAppraisalPeriodDto> { Items = list.MapTo<List<QCAppraisalPeriodDto>>(), TotalCount = count };

        }

        public void CreatePeriod(QCAppraisalPeriodDto input)
        {
            var period = input.MapTo<QCAppraisalPeriod>();
            period.Status = QCAppraisalPeriodStatus.发布;
            period.CreatorDepartmentId = int.Parse(AbpSession.GetDepartmentId());
            _qCAppraisalPeriodRepository.Insert(period);
        }

        public void UpdatePeriod(QCAppraisalPeriodDto input)
        {
            var period = _qCAppraisalPeriodRepository.Get(input.Id);
            if (period == null)
                throw new UserFriendlyException(-1, "该考核周期不存在");
            input.CreationTime = period.CreationTime.ToString();           
            ObjectMapper.Map(input, period); 
            _qCAppraisalPeriodRepository.Update(period);
        }

        public QCAppraisalPeriodDto GetPeriod(int Id)
        {
            return _qCAppraisalPeriodRepository.Get(Id).MapTo<QCAppraisalPeriodDto>(); 
        }

        public IEnumerable<QCDetailDto> GetDetailsByPeriod(int Id)
        {
            var query = _qCAppraisalDetailRepository.GetAllList().Where(T =>T.DepartmentPunishmentPeriodId ==Id).OrderBy(T=>T.FunctionalDepartmentId);
            return query.MapTo<IEnumerable<QCDetailDto>>();

        }

        public void CreateDetail(QCDetailDto input)
        {
            input.FunctionalDepartmentId =int.Parse(AbpSession.GetDepartmentId());
            var detail = input.MapTo<QCAppraisalDetail>();
            _qCAppraisalDetailRepository.Insert(detail);
        }

        public void RemoveDetail(int Id)
        {
            var detail = _qCAppraisalDetailRepository.Get(Id);
            if(detail.FunctionalDepartmentId.ToString()!=AbpSession.GetDepartmentId())
                throw new UserFriendlyException("登录科室与督察科室不一致！");
            _qCAppraisalDetailRepository.Delete(Id);
        }


        public void Test()
        {
            _homePageDomianService.SynchronousHomePage("2019-03-01", "2019-03-01");

        }
    }
}
