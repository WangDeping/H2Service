using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using H2Service.Dto;
using H2Service.External.Dto;
using H2Service.MedicalWastes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.External
{
  public  class ExternalCompanyAppService:H2ServiceAppServiceBase,IExternalCompanyAppService
    {
        private readonly IRepository<ExternalCompany> _externalCompanyRepository;

        public ExternalCompanyAppService(IRepository<ExternalCompany> externalCompanyRepository)
        {
            _externalCompanyRepository = externalCompanyRepository;
        }


        public  PagedResultDto<ExternalCompanyDto> GetPagedExternalCompanies(PagedInputDto input)
        {
            var query = _externalCompanyRepository.GetAll();
            var companiesCount = query.Count();
            var companiesList = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<ExternalCompanyDto> { Items = companiesList.MapTo<List<ExternalCompanyDto>>(), TotalCount = companiesCount };
        }

        public void CreateExternalCompany(ExternalCompanyDto input) {

            _externalCompanyRepository.Insert(input.MapTo<ExternalCompany>());
        }

        public void UpdateExternalCompany(ExternalCompanyDto input)
        {
            _externalCompanyRepository.Update(input.MapTo<ExternalCompany>());
        }

        public void RemoveExternalCompany(int Id)
        {
            _externalCompanyRepository.Delete(Id);
        }

        public ExternalCompanyDto GetExternalCompany(int Id)
        {
            var company = _externalCompanyRepository.Get(Id);
            return company.MapTo<ExternalCompanyDto>();
        }
    }
}
