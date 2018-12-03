using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Events.Bus;
using Abp.UI;
using AutoMapper;
using H2Service.Authorization;
using H2Service.Dto;
using H2Service.Events;
using H2Service.Events.Handler;
using H2Service.Extensions;
using H2Service.Salaries.Dto;
using H2Service.Salarires;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H2Service.Salaries
{
    public class SalaryAppService : H2ServiceAppServiceBase,ISalaryAppService
    {
        private readonly IRepository<SalaryPeriod> _salaryPeriodRepository;
        private readonly IRepository<SalaryDetail> _salaryDetailRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<SalaryType> _salaryTypeRepository;
        private readonly ISalaryDomainService _salaryDomainService;
        private readonly IEventBus _eventsBus;
        public SalaryAppService(
                IRepository<SalaryPeriod> salaryPeriodRepository,
                IRepository<SalaryDetail> salaryDetailRepository,
                IUnitOfWorkManager unitOfWorkManager,
                IRepository<SalaryType> salaryTypeRepository,
                ISalaryDomainService salaryDomainService,
                IEventBus eventsBus)
        {
            _salaryDetailRepository = salaryDetailRepository;
            _salaryPeriodRepository = salaryPeriodRepository;
            _unitOfWorkManager = unitOfWorkManager;
            _salaryTypeRepository = salaryTypeRepository;
            _salaryDomainService = salaryDomainService;
            _eventsBus =eventsBus;           
        }
      
        public IEnumerable<SalaryType> GetAllTypes()
        {
            return _salaryTypeRepository.GetAll().ToList();
        }

        [UnitOfWork]
        [AbpAuthorize(PermissionNames.Pages_Salary_Upload)]
        public void UploadSalary(CreateSalaryPeriodInput input)
        {
            var detailsDtoList = input.SalaryDetailList;
            if(detailsDtoList.Count()==0)
                throw new UserFriendlyException("工资表中没有数据.");
            var period_find = _salaryPeriodRepository.FirstOrDefault(
                p => p.Period == input.Period
                && p.SalaryTypeID == input.SalaryTypeID);
            if (period_find != null)
                throw new UserFriendlyException("该期工资已经上传");         
            var period= _salaryPeriodRepository.Insert(input.MapTo<SalaryPeriod>());
           
            _salaryDomainService.AcceptSalaryDetail(period,Mapper.Map<List< SalaryDetail >> (detailsDtoList));
           
        }
        /// <summary>
        /// 获取历史工资(分页)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize(PermissionNames.Pages_Salary_Upload)]
        public PagedResultDto<SalaryPeriodDto> GetPagedSalaryPeriods(PagedInputDto input)
        {
            var query = _salaryPeriodRepository.GetAll();
            var periodsCount = query.Count();
            var periodList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).Select(t => new SalaryPeriodDto {
                Period = t.Period,
                SalaryTypeName = t.SalaryType.SalaryTypeName,
                Id = t.Id,
                CreationTime = t.CreationTime.ToString(),
                UserName=t.CreatorUser.UserName
            }).ToList();            
            return new PagedResultDto<SalaryPeriodDto> { Items =periodList , TotalCount = periodsCount };
        }
        /// <summary>
        /// 个人的工资历史明细
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public PagedResultDto<SalaryDetailDto> GetPagedPersonalSalaryPeriods(PagedInputDto input)
        {
            var userNumber = AbpSession.GetUserNumber();
           
            var query =_salaryDetailRepository.GetAll().Where(s=>s.UserNumber==userNumber);
            var periodsCount = query.Count();
         
            var periodList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).Select(t => new SalaryDetailDto
            {
                Period = t.SalaryPeriod.Period,
                CreatorUser=t.SalaryPeriod.CreatorUser.UserName,
                SalaryTypeName = t.SalaryPeriod.SalaryType.SalaryTypeName,
                Id = t.Id,
                CreationTime = t.SalaryPeriod.CreationTime.ToString()                
            }).ToList();
            return new PagedResultDto<SalaryDetailDto> { Items = periodList, TotalCount = periodsCount };
        }
        /// <summary>
        /// 个人查看资明细,用于手机查看
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>      
        [AbpAuthorize]
        public PersonalSalaryOutput GetPersonSalary(PersonalSalaryInput input)
        {
            var salaryDetail= _salaryDetailRepository.FirstOrDefault(T => T.UserNumber == input.UserNumber&&T.SalaryPeriodID==input.PeriodId);
            if (salaryDetail == null)
                throw new UserFriendlyException("查无此人");           
            return new PersonalSalaryOutput() { Detail = salaryDetail.Detail, SalaryType = salaryDetail.SalaryPeriod.SalaryType, UserNumber = salaryDetail.UserNumber };
        }
        /// <summary>
        /// 根据SalaryDetail Id获取工资明细
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        [AbpAuthorize]
        public PersonalSalaryOutput GetPersonSalary(int detailId)
        {
            var salaryDetail = _salaryDetailRepository.FirstOrDefault(T => T.Id==detailId);
            if (salaryDetail == null)
                throw new UserFriendlyException("查无此人");
            if (salaryDetail.UserNumber != AbpSession.GetUserNumber())
                throw new UserFriendlyException("您查看的并非本人工资");
            return new PersonalSalaryOutput() { Detail = salaryDetail.Detail };
        }
    }
}
