using Abp.Application.Services;
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Domain.Repositories;
using H2Service.Account.Dto;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account
{
 public   class DistrictAppService : H2ServiceAppServiceBase, IDistrictAppService
    {
        private readonly IRepository<District> _districtRepository;

        public DistrictAppService(IRepository<District> districtRepository)
        {
            _districtRepository = districtRepository;

        }

        public IEnumerable<DistrictDto> GetDistrictList()
        {
            var list= _districtRepository.GetAllList();
            if (list == null)
                return new List<DistrictDto>();
            else
                return list.MapTo<List<DistrictDto>>();
        }
    }
}
