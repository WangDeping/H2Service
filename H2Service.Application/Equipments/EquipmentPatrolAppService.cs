using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using H2Service.EnumDic;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public   class EquipmentPatrolAppService: H2ServiceAppServiceBase, IEquipmentPatrolAppService
    {
        private readonly IRepository<EquipmentPatrolLog> _equipmentPatrolRepository;
        private readonly IRepository<EquipmentPatrolDetail> _equipmentPatrolDetailRepository;
        private readonly IRepository<Equipment> _equipmentRepository;
        public EquipmentPatrolAppService(IRepository<EquipmentPatrolLog> equipmentPatrolRepository,
             IRepository<EquipmentPatrolDetail> equipmentPatrolDetailRepository,
             IRepository<Equipment> equipmentRepository
            ) {

            _equipmentPatrolRepository = equipmentPatrolRepository;
            _equipmentPatrolDetailRepository = equipmentPatrolDetailRepository;
            _equipmentRepository = equipmentRepository;
        }

        public void CreatePatrol(CreatePatrolInput input)
        {            
            var patrolLog = input.MapTo<EquipmentPatrolLog>();
            patrolLog.CreationTime = DateTime.Now;
            patrolLog.PatrolDetails.Clear() ;  
           
            var logId = _equipmentPatrolRepository.InsertAndGetId(patrolLog);            
            foreach (var detail in input.PatrolDetails) {
                Logger.Error(logId + ":" + detail.PropertyId + ":" + detail.Result);
                 _equipmentPatrolDetailRepository.Insert(new EquipmentPatrolDetail
                 {
                     PatrolId = logId,
                     PropertyId = detail.PropertyId,
                     Result = detail.Result
                 });
            
             }
            var equipment = _equipmentRepository.Get(input.EquipmentId);
            if (!patrolLog.MainCheck)
                equipment.Status = EquipmentStatus.维修;            
            else
                equipment.Status = EquipmentStatus.完好;
            _equipmentRepository.Update(equipment);
        }
        /// <summary>
        /// 巡视记录检索
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultDto<EquipmentPatrolLogDto> GetPatrolLogs(GetPatrolLogsInput input)
        {
            var query =_equipmentPatrolRepository.GetAll().Where(T=>T.CreationTime>=input.Begin&&T.CreationTime<=input.End)
                .WhereIf(!string.IsNullOrEmpty(input.Code), T => T.Equipment.Code== input.Code)
                .WhereIf(input.Type!=PatrolTypeEnum.不区分,T=>T.Type==input.Type)
                .WhereIf(input.DepartmentId != null, T => T.Equipment.DepartmentId == input.DepartmentId).ToList();
            var count = query.Count();
            var pageResult = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<EquipmentPatrolLogDto> { Items = pageResult.MapTo<List<EquipmentPatrolLogDto>>(), TotalCount = count };
        }
    }
}
