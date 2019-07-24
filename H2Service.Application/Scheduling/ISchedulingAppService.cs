using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.H2Modules;
using H2Service.Scheduling.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling
{
 public  interface ISchedulingAppService: IApplicationService
    {
        List<SchedulingTypeGroupDto> GetSchedulingTypeGroups();

        void CreateTypeGroup(string groupName);

        void UpdateTypeGroup(SchedulingTypeGroupDto input);

        List<SchedulingTypeDto> GetSchedulingTypes(int groupId = 0);

        SchedulingTypeDto GetSchedulingType(int typeId);

       

        void CreateType(SchedulingTypeDto input);

        void UpdateType(SchedulingTypeDto input);

        IEnumerable<H2ModuleWithAuditingDto> GetDepartmentsBySchedulingUserId(long userId);

        IEnumerable<SchedulingDepartmentUserDto> GetSchedulingDepartmentUsers(int departmentId);
        /// <summary>
        /// 添加排班科室与人员对应
        /// </summary>
        /// <param name="input">入参</param>
       void CreateDepartmentUser(CreateDepartmentUserInput input);
        /// <summary>
        /// 删除排班科室与人员对应
        /// </summary>
        /// <param name="input">入参</param>
        void RemoveDepartmentUser(RemoveDepartmentUserInput input);
    }
}
