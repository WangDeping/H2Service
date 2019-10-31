using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public   class EquipmentKindTypeAppService: H2ServiceAppServiceBase,IEquipmentKindTypeAppService
    {
        private readonly IRepository<EquipmentKind> _kindRepository;
        private readonly IRepository<EquipmentType> _typeRepository;
        private readonly IRepository<EquipmentModel> _modelRepository;
        public EquipmentKindTypeAppService(IRepository<EquipmentType> typeRepository, 
            IRepository<EquipmentKind> kindRepository, IRepository<EquipmentModel> modelRepository) {
            _typeRepository = typeRepository;
            _kindRepository = kindRepository;
            _modelRepository = modelRepository;
        }

        public void CreateEqType(EquipmentTypeDto dto) {
            if (_typeRepository.FirstOrDefault(T => T.EquipmentTypeName == dto.EquipmentTypeName)!=null)
                throw new UserFriendlyException("类型名称已经存在");
            _typeRepository.Insert(dto.MapTo<EquipmentType>());
        }
        public IList<EquipmentTypeDto> GetEqTypeList() {
            return _typeRepository.GetAllList().MapTo<List<EquipmentTypeDto>>();
        }
        public void CreateEqKind(EquipmentKindDto dto) {
            if(_kindRepository.FirstOrDefault(T=>T.EquipmentKindName==dto.EquipmentKindName)!=null)
                throw new UserFriendlyException("种类名称已经存在");
            _kindRepository.Insert(dto.MapTo<EquipmentKind>());
        }
        public IList<EquipmentKindDto> GetEqKindList()
        {
            return _kindRepository.GetAllList().MapTo<List<EquipmentKindDto>>();
        }
        public IList<EquipmentModelDto> GetEqModelList()
        {
            return _modelRepository.GetAllList().MapTo<List<EquipmentModelDto>>();
        }
    }
}
