using Abp.Domain.Repositories;
using Abp.Domain.Services;
using Castle.Core.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Departments
{
    public class H2ModuleDomainService : DomainService
    {
        private readonly IRepository<DepartmentRelateModule> _moduleRepository;
        private readonly IRepository<H2ModuleWithAuditing> _h2ModuleWithAuditingRepository;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<UserPermission> _userPermissionRepository;
        //private ILogger  Logger{ get; set; }
        public H2ModuleDomainService(IRepository<DepartmentRelateModule> moduleRepository,
            IRepository<H2ModuleWithAuditing> h2ModuleWithAuditingRepository,
            IRepository<User, long> userRepository,
            IRepository<UserPermission> userPermissionRepository)
        {
            _moduleRepository = moduleRepository;
            _h2ModuleWithAuditingRepository = h2ModuleWithAuditingRepository;
            _userPermissionRepository = userPermissionRepository;
            _userRepository = userRepository;
        }
        /// <summary>
        /// 设置或修改操作人员及分配权限
        /// </summary>
        /// <param name="h2ModuleWithAuditing"></param>
        /// <param name="doPermission"></param>
        public void SetDoUserWithPermission(H2ModuleWithAuditing h2ModuleWithAuditing,string doPermission)
        {
            Logger.Error("序列化" + JsonConvert.SerializeObject(h2ModuleWithAuditing));
            if (h2ModuleWithAuditing.DoUserId <= 0)
                return;
            if (h2ModuleWithAuditing.Id>0)
            {//修改操作人员
                var primaryModuleDepartment = _h2ModuleWithAuditingRepository.Get(h2ModuleWithAuditing.Id);
                //改操作人员
                if (h2ModuleWithAuditing.DoUserId != primaryModuleDepartment.DoUserId)
                {
                    //要被改变的操作人员只有一条记录，则删掉其操作权限
                    if (_h2ModuleWithAuditingRepository.GetAll().Where(T => T.DoUserId == primaryModuleDepartment.DoUserId&&T.Module==h2ModuleWithAuditing.Module).Count() == 1)
                    {
                        _userPermissionRepository.Delete(T => T.UserId == primaryModuleDepartment.DoUserId && T.PermissionName == doPermission);
                    }
                    //要改变成的操作人员没有记录(之前没有分配过任何科室的操作关联)，则分配操作Do权限
                    if (_h2ModuleWithAuditingRepository.GetAll().Where(T => T.DoUserId==h2ModuleWithAuditing.DoUserId&&T.Module==h2ModuleWithAuditing.Module).Count() == 0)
                    {
                        var userPermission = _userRepository.Get(h2ModuleWithAuditing.DoUserId.Value).Permissions;
                        userPermission.Add(new UserPermission { PermissionName = doPermission, UserId = h2ModuleWithAuditing.DoUserId.Value });
                    }
                    primaryModuleDepartment.DoUserId= h2ModuleWithAuditing.DoUserId;
                }
                             
            }
            else
            {//创建科室的操作人员
                //确保每个科室不重复出现
                var deleteModuleDepartment = _h2ModuleWithAuditingRepository.FirstOrDefault(t => t.DepartmentId == h2ModuleWithAuditing.DepartmentId && t.Module == h2ModuleWithAuditing.Module);
                if (deleteModuleDepartment != null)
                    _h2ModuleWithAuditingRepository.Delete(deleteModuleDepartment);

                    var userPermission = _userRepository.Get(h2ModuleWithAuditing.DoUserId.Value).Permissions;
                    //分配操作权限
                    if (userPermission.FirstOrDefault(T => T.PermissionName == doPermission) == null)
                        userPermission.Add(new UserPermission { PermissionName = doPermission, UserId =h2ModuleWithAuditing.DoUserId.Value });
             
           
               _h2ModuleWithAuditingRepository.Insert(h2ModuleWithAuditing);
            }

        }
        /// <summary>
        /// 设置或修改审核人员及分配权限
        /// </summary>
        /// <param name="h2ModuleWithAuditing"></param>
        /// <param name="auditorPermission"></param>
        public void SetAuditorWithPermission(H2ModuleWithAuditing h2ModuleWithAuditing, string auditorPermission) {
            Logger.Error("序列化" + JsonConvert.SerializeObject(h2ModuleWithAuditing));
            if (h2ModuleWithAuditing.AuditorId <= 0)
                return;
                if (h2ModuleWithAuditing.Id > 0)
                    {//修改审核人员
                        var primaryModuleDepartment = _h2ModuleWithAuditingRepository.Get(h2ModuleWithAuditing.Id);
                        //改操作人员
                        if (h2ModuleWithAuditing.AuditorId != primaryModuleDepartment.AuditorId)
                        {
                            //要被改变的审核人员只有一条记录，则删掉其操作权限
                            if (_h2ModuleWithAuditingRepository.GetAll().Where(T => T.AuditorId == primaryModuleDepartment.AuditorId && T.Module == h2ModuleWithAuditing.Module).Count() == 1)
                            {
                                _userPermissionRepository.Delete(T => T.UserId == primaryModuleDepartment.AuditorId && T.PermissionName == auditorPermission);
                            }
                            //要改变成的审核人员没有记录(之前没有分配过任何科室的操作关联)，则分配审核权限
                            if (_h2ModuleWithAuditingRepository.GetAll().Where(T => T.AuditorId == h2ModuleWithAuditing.AuditorId && T.Module == h2ModuleWithAuditing.Module).Count() == 0)
                            {
                                var userPermission = _userRepository.Get(h2ModuleWithAuditing.AuditorId.Value).Permissions;
                                userPermission.Add(new UserPermission { PermissionName = auditorPermission, UserId = h2ModuleWithAuditing.AuditorId.Value });
                            }
                            primaryModuleDepartment.AuditorId = h2ModuleWithAuditing.AuditorId;
                        }


            }
            else {
                var deleteModuleDepartment = _h2ModuleWithAuditingRepository.FirstOrDefault(t => t.DepartmentId == h2ModuleWithAuditing.DepartmentId && t.Module == h2ModuleWithAuditing.Module);
                if (deleteModuleDepartment != null)
                    _h2ModuleWithAuditingRepository.Delete(deleteModuleDepartment);
                var userPermission = _userRepository.Get(h2ModuleWithAuditing.AuditorId.Value).Permissions;
                //分配审核权限
                if (userPermission.FirstOrDefault(T => T.PermissionName == auditorPermission) == null)
                    userPermission.Add(new UserPermission { PermissionName = auditorPermission, UserId = h2ModuleWithAuditing.AuditorId.Value });

                _h2ModuleWithAuditingRepository.Insert(h2ModuleWithAuditing);
            }
        }
    }
}
