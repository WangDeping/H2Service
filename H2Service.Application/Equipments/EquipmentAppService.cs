using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.EnumDic;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    public class EquipmentAppService : H2ServiceAppServiceBase, IEquipmentAppService
    {
        private readonly IRepository<Equipment> _equipmentRepository;
       
        public EquipmentAppService(IRepository<Equipment> equipmentRepository) {
            _equipmentRepository = equipmentRepository;
        }
        /// <summary>
        /// 添加设备信息
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public int CreateEquipment(CreateEquipmentInput input) {
            if (_equipmentRepository.FirstOrDefault(T => T.Code == input.Code) != null)
                throw new UserFriendlyException("条码已经存在");
            var equipment = input.MapTo<Equipment>();
            equipment.Status = EquipmentStatus.完好;
            return _equipmentRepository.InsertAndGetId(equipment);
        }
        /// <summary>
        /// 修改设备
        /// </summary>
        /// <param name="input"></param>
        public void UpdateEquipment(CreateEquipmentInput input) {
            var equipment = _equipmentRepository.Get(input.Id);          
            ObjectMapper.Map(input, equipment);
            _equipmentRepository.Update(equipment);
        }

        public PagedResultDto<EquipmentOutput> GetPagedEquipment(GetEquipmentInput input) {
            var query = _equipmentRepository.GetAll().WhereIf(input.DepIds!=null&&input.DepIds.Count>0,T=>input.DepIds.Contains(T.DepartmentId));
            var count = query.Count();
            var pageResult = query.OrderByDescending(T => T.Id).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<EquipmentOutput> { Items = pageResult.MapTo<List<EquipmentOutput>>(), TotalCount = count };
        }

        public EquipmentOutput GetEquipment(GetEquipmentInput input) {
            if (input.Id == null && string.IsNullOrEmpty(input.Code))
                throw new UserFriendlyException("请输入条码号");
            else {
                var equipment= _equipmentRepository.GetAll().WhereIf(input.Id != null&&input.Id!=0, T => T.Id == input.Id)
                    .WhereIf(!string.IsNullOrEmpty(input.Code), T => T.Code == input.Code)
                    .FirstOrDefault();
                if (equipment == null)
                    throw new UserFriendlyException("未匹配到设备");
                var output = equipment.MapTo<EquipmentOutput>();
                output.Properties = equipment.Type.EquipmentProperties.ToList().MapTo<List<EquipmentPropertyDto>>();               
                return output;
            }
        }
        /// <summary>
        /// 查询需要巡检设备
        /// </summary>
        /// <param name="freq">巡检频次</param>
        /// <param name="patrolType">巡检级别</param>
        /// <param name="notPatrol">是否查询未巡检设备</param>
        /// <returns></returns>
        public IList<EquipmentOutput> GetNeedPatrolEquipment(GetNeedPatrolEquipmentInput input) {

            var result = _equipmentRepository.GetAll().Where(T => T.Type.EquipmentProperties.Any(t => t.Frequency == input.Freq)&&T.Status==input.Status)
                .ToList();
            if (!input.NotPatrol)
                return result.MapTo<List<EquipmentOutput>>();
            else
            {
                var today = DateTime.Now.Date;
                switch (input.Freq) {
                    case EquipmentPartrolFrequencyEnum.每日:
                        return result.MapTo<List<EquipmentOutput>>().Where(T=>!T.HasPatrol_I).ToList();                  
                    case EquipmentPartrolFrequencyEnum.每月:    
                        return result.MapTo<List<EquipmentOutput>>().Where(T => !T.HasPatrol_II).ToList();
                    default:
                        return result.MapTo<List<EquipmentOutput>>();                       

                }

            }
                
        }

      
    }
}
