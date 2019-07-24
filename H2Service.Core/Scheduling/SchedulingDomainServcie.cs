using Abp.Domain.Repositories;
using Abp.Domain.Services;
using H2Service.Authorization;
using H2Service.Authorization.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Scheduling
{
 public   class SchedulingDomainServcie: DomainService
    {
        //private readonly IRepository<DepartmentRelateModule> _moduleRepository;
        //private readonly IRepository<SchedulingDepartment> _schedulingDepartmentRepository;
        //private readonly IRepository<User, long> _userRepository;
        //private readonly IRepository<UserPermission> _userPermissionRepository;
        //public SchedulingDomainServcie(IRepository<DepartmentRelateModule> moduleRepository,
        //    IRepository<SchedulingDepartment> schedulingDepartmentRepository,
        //    IRepository<User, long> userRepository,
        //    IRepository<UserPermission> userPermissionRepository) {
        //    _moduleRepository = moduleRepository;
        //    _schedulingDepartmentRepository = schedulingDepartmentRepository;
        //    _userPermissionRepository = userPermissionRepository;
        //    _userRepository = userRepository;
        //}

        //public void SetSchedulingDepartmentWithPermission(SchedulingDepartment schedulingDepartment)
        //{
        //    var auditorPermission = PermissionNames.Pages_Scheduling_Auditor;
        //    var doPermission = PermissionNames.Pages_Scheduling_Do;
        //    if (schedulingDepartment.Id > 0)
        //    {//修改排班/审核人员
        //        var primarySchedulingDepartment = _schedulingDepartmentRepository.Get(schedulingDepartment.Id);
        //        //改排班人员
        //        if (schedulingDepartment.ShedulingUserId > 0&&(schedulingDepartment.ShedulingUserId!=primarySchedulingDepartment.ShedulingUserId)) {//修改排班
        //            //要被改变的排班人员只有一条记录，则删掉其排班权限
        //            if (_schedulingDepartmentRepository.GetAll().Where(T => T.ShedulingUserId ==primarySchedulingDepartment.ShedulingUserId).Count()== 1)
        //            {                      
        //                _userPermissionRepository.Delete(T => T.UserId == primarySchedulingDepartment.ShedulingUserId&&T.PermissionName==doPermission);
        //            }
        //            //要改变成的排班人员没有记录(之前没有分配过任何科室的排班关联)，则分配排班权限
        //            if (_schedulingDepartmentRepository.GetAll().Where(T => T.ShedulingUserId ==schedulingDepartment.ShedulingUserId).Count() == 0)
        //            {
        //                var userPermission = _userRepository.Get(schedulingDepartment.ShedulingUserId.Value).Permissions;
        //                userPermission.Add(new UserPermission { PermissionName =doPermission, UserId = schedulingDepartment.ShedulingUserId.Value });
        //            }
        //            primarySchedulingDepartment.ShedulingUserId = schedulingDepartment.ShedulingUserId;
        //        }
        //        //改审核人员
        //        if (schedulingDepartment.AuditorId> 0 && (schedulingDepartment.AuditorId!= primarySchedulingDepartment.AuditorId))
        //        {//修改排班
        //            //要被改变的审核人员只有一条记录，则删掉其审核权限
        //            if (_schedulingDepartmentRepository.GetAll().Where(T => T.AuditorId== primarySchedulingDepartment.AuditorId).Count() == 1)
        //            {
        //                _userPermissionRepository.Delete(T => T.UserId == primarySchedulingDepartment.AuditorId && T.PermissionName ==auditorPermission);
        //            }
        //            //要改变成的审核人员没有记录(之前没有分配过任何科室的排班关联)，则审核排班权限
        //            if (_schedulingDepartmentRepository.GetAll().Where(T => T.AuditorId == schedulingDepartment.AuditorId).Count() == 0)
        //            {
        //                var userPermission = _userRepository.Get(schedulingDepartment.AuditorId.Value).Permissions;
        //                userPermission.Add(new UserPermission { PermissionName =auditorPermission, UserId = schedulingDepartment.AuditorId.Value });
        //            }
        //            primarySchedulingDepartment.AuditorId = schedulingDepartment.AuditorId;
        //        }
        //    }
        //    else {//创建科室的排班/审核人员
        //        //确保每个科室不重复出现
        //        var deleteSchedulingDepartment = _schedulingDepartmentRepository.FirstOrDefault(t=>t.DepartmentId==schedulingDepartment.DepartmentId);
        //        if (deleteSchedulingDepartment != null)
        //            _schedulingDepartmentRepository.Delete(deleteSchedulingDepartment);
        //        var userPermission = _userRepository.Get(schedulingDepartment.ShedulingUserId.Value).Permissions;
        //        if (schedulingDepartment.ShedulingUserId > 0) {               
        //            //分配排班权限
        //            if (userPermission.FirstOrDefault(T => T.PermissionName == doPermission) == null)
        //                userPermission.Add(new UserPermission { PermissionName = doPermission, UserId = schedulingDepartment.ShedulingUserId.Value });
        //        }
        //        if (schedulingDepartment.AuditorId > 0) {                
        //            //分配排班权限
        //            if (userPermission.FirstOrDefault(T => T.PermissionName == auditorPermission) == null)
        //                userPermission.Add(new UserPermission { PermissionName = auditorPermission, UserId = schedulingDepartment.ShedulingUserId.Value });
        //        }
        //        _schedulingDepartmentRepository.Insert(schedulingDepartment);
        //    }

        //}
    }
}
