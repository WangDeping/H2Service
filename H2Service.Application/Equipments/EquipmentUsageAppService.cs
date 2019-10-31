using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using H2Service.Equipments.Dto;
using H2Service.MedicalData.HomePages;
using H2Service.MedicalData.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public   class EquipmentUsageAppService:H2ServiceAppServiceBase,IEquipmentUsageAppService
    {
        private readonly IRepository<EquipmentUsageLog> _usageRepository;
        private readonly UserDomainService _patientDomainService;
        public EquipmentUsageAppService(IRepository<EquipmentUsageLog> usageRepository,
            UserDomainService patientDomainService) {
            _usageRepository = usageRepository;
            _patientDomainService=patientDomainService;
        }
        public void CreateUsage(CreateUsageInput input) {
            if (_usageRepository.FirstOrDefault(T => T.EquipmentId == input.EquipmentId && T.EndTime == null) != null)
                throw new Exception("该设备已经在使用");
            var usage = input.MapTo<EquipmentUsageLog>();
            if (!string.IsNullOrEmpty(input.PatientAdmNo)) {
                var patient = _patientDomainService.GetPatientSample(input.PatientAdmNo);
                usage.PatientName = patient.PatName;
                usage.Diagnose = patient.Diagnose;
            }
            _usageRepository.Insert(usage);
        }

        public void EndUsage(EndUsageInput input) {
            var usage = _usageRepository.Get(input.Id);
            usage.EndUserId = input.EndUserId;
            usage.EndTime = input.EndTime;
            _usageRepository.Update(usage);
        }
        /// <summary>
        /// 获取使用记录
        /// </summary>
        /// <param name="input">分页参数,Id</param>
        /// <returns></returns>
        public PagedResultDto<EquipmentUsageLogDto> GetPatrolLogs(GetUsagelLogsInput input)
        {
            var query = _usageRepository.GetAllList(T => T.EquipmentId == input.EquipmentId);
            var count = query.Count();           
            var pageResult = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<EquipmentUsageLogDto> { Items = pageResult.MapTo<List<EquipmentUsageLogDto>>(), TotalCount = count };
        }
    }
}
