using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.UI;
using H2Service.Authorization.Departments;
using H2Service.H2Modules;
using H2Service.Scheduling.Dto;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace H2Service.Scheduling
{
    public   class SchedulingAppService:H2ServiceAppServiceBase,ISchedulingAppService
    {
        private readonly IRepository<SchedulingGroup> _schedulingGroupRepository;
        private readonly IRepository<SchedulingType> _schedulingTypeRepository;
        private readonly IRepository<SchedulingRecord> _schedulingRecordRepository;
        //private readonly IRepository<SchedulingDepartment> _schedulingDepartmentRepository;
        private readonly IRepository<H2ModuleWithAuditing> _h2ModuleWithAuditingRepository;
      
        private readonly IRepository<SchedulingDepartmentUser> _schedulingDepartmentUserRepository;
        private readonly SchedulingDomainServcie _schedulingDomainService;
        public SchedulingAppService(IRepository<SchedulingGroup> schedulingGroupRepository,
            IRepository<SchedulingType> schedulingTypeRepository,
            IRepository<SchedulingRecord> schedulingRecordRepository,          
            IRepository<H2ModuleWithAuditing> h2ModuleWithAuditingRepository,         
            IRepository<SchedulingDepartmentUser> schedulingDepartmentUserRepository,
            SchedulingDomainServcie schedulingDomainService) {

            _schedulingGroupRepository = schedulingGroupRepository;
            _schedulingRecordRepository = schedulingRecordRepository;
            _schedulingTypeRepository = schedulingTypeRepository;          
            _h2ModuleWithAuditingRepository = h2ModuleWithAuditingRepository;      
            _schedulingDepartmentUserRepository = schedulingDepartmentUserRepository;
            _schedulingDomainService = schedulingDomainService;
        }


        public List<SchedulingTypeGroupDto> GetSchedulingTypeGroups() {
          var result= _schedulingGroupRepository.GetAllList();        
           return result.MapTo<List<SchedulingTypeGroupDto>>();
        }
        /// <summary>
        /// 创建班次类型分组
        /// </summary>
        /// <param name="groupName"></param>
        public void CreateTypeGroup(string groupName)
        {
            var group = _schedulingGroupRepository.FirstOrDefault(T => T.SchedulingGroupName == groupName);
            if (group != null)
                throw new UserFriendlyException("分组名称重复");
            _schedulingGroupRepository.Insert(new SchedulingGroup { SchedulingGroupName = groupName });

        }
        /// <summary>
        /// 修改类型分组
        /// </summary>
        /// <param name="input"></param>
        public void UpdateTypeGroup(SchedulingTypeGroupDto input)
        {
            var result = _schedulingGroupRepository.Get(input.Id);
            if (result == null)
                throw new UserFriendlyException("此类型不存在或已被删除");            
            result.SchedulingGroupName = input.SchedulingGroupName;

        }

        public List<SchedulingTypeDto> GetSchedulingTypes(int groupId = 0) {
            var result = groupId == 0 ? _schedulingTypeRepository.GetAllList() : _schedulingGroupRepository.Get(groupId).SchedulingTypes.ToList();
            return result.MapTo<List<SchedulingTypeDto>>();
        }
        /// <summary>
        /// 获取班次
        /// </summary>
        /// <param name="typeId">Id</param>
        /// <returns></returns>
        public SchedulingTypeDto GetSchedulingType(int typeId) {
            var result = _schedulingTypeRepository.Get(typeId);        
            return result.MapTo<SchedulingTypeDto>();
        }
        /// <summary>
        /// 创建班次类型
        /// </summary>
        /// <param name="input"></param>
        public void CreateType(SchedulingTypeDto input) {
            if (input != null)
                if (input.SchedulingTypeName.Length > 5)
                    throw new UserFriendlyException("名称长度不能超过5");
                if(_schedulingTypeRepository.FirstOrDefault(T=>T.SchedulingTypeName==input.SchedulingTypeName)!=null)
                    throw new UserFriendlyException("名称重复");
            _schedulingTypeRepository.Insert(input.MapTo<SchedulingType>());
        }
        /// <summary>
        /// 修改班次类型
        /// </summary>
        /// <param name="input"></param>
        public void UpdateType(SchedulingTypeDto input)
        {
            var result = _schedulingTypeRepository.GetAll().AsNoTracking().FirstOrDefault(T=>T.SchedulingTypeName==input.SchedulingTypeName);
            if(result!=null&&(result.Id!=input.Id))
                throw new UserFriendlyException("名称重复");            
            _schedulingTypeRepository.Update(input.MapTo<SchedulingType>());
        }
       
         /// <summary>
        /// 获取该具有排班权限用户下的排班科室
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public IEnumerable<H2ModuleWithAuditingDto> GetDepartmentsBySchedulingUserId(long userId) {
            var departments =_h2ModuleWithAuditingRepository.GetAll().Where(T => T.DoUserId== userId&&T.Module==H2Module.需排班科室);
            return departments.MapTo<IEnumerable<H2ModuleWithAuditingDto>>();
        }
        /// <summary>
        /// 获取属于该部门下的排班对象,科室与人员所属科室可以不一致
        /// </summary>
        /// <param name="departmentId">具有该科室排班权限人员可见科室Id</param>
        /// <returns></returns>
        public IEnumerable<SchedulingDepartmentUserDto> GetSchedulingDepartmentUsers(int departmentId)
        {
            var departmentUsers = _schedulingDepartmentUserRepository.GetAll().Where(T=>T.DepartmentId==departmentId);
           
            return departmentUsers.MapTo<IEnumerable<SchedulingDepartmentUserDto>>();
        }
        /// <summary>
        /// 添加排班科室与人员对应
        /// </summary>
        /// <param name="input"></param>
        public void CreateDepartmentUser(CreateDepartmentUserInput input) {
            if (_schedulingDepartmentUserRepository.FirstOrDefault(T => T.UserId == input.UserId && T.DepartmentId == input.DepartmentId) != null)
                throw new UserFriendlyException("该人员已经在此排班科室");
            var departmentUser = _schedulingDepartmentUserRepository.FirstOrDefault(T => T.UserId == input.UserId);     
             if(!input.Mandatory&&departmentUser!=null)
                throw new UserFriendlyException("该人员已经在" + departmentUser.Department.DepartmentName + "中");
            else
            {
                var departmentUserDto = new SchedulingDepartmentUserDto
                {
                    DepartmentId = input.DepartmentId,
                    UserId = input.UserId
                };
                _schedulingDepartmentUserRepository.Insert(departmentUserDto.MapTo<SchedulingDepartmentUser>());
            }
        }
        /// <summary>
        /// 删除排班科室与人员对应
        /// </summary>
        /// <param name="input"></param>
        public void RemoveDepartmentUser(RemoveDepartmentUserInput input) {
            var depUser = _schedulingDepartmentUserRepository.FirstOrDefault(T => T.DepartmentId == input.DepartmentId && T.UserId == input.UserId);
            if (depUser != null)
                _schedulingDepartmentUserRepository.Delete(depUser);
        }
    }
}
