using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using H2Service.Authorization.Departments;
using H2Service.H2Modules.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.H2Modules
{
 public   class H2ModuleAppService : H2ServiceAppServiceBase
    {
        private readonly IRepository<H2ModuleWithAuditing> _auditingRepository;
        private readonly IRepository<DepartmentRelateModule> _moduleRepository;
        private readonly H2ModuleDomainService _h2ModuleDomainService;

        public H2ModuleAppService(IRepository<H2ModuleWithAuditing> auditingRepository,
           IRepository<DepartmentRelateModule> moduleRepository,
           H2ModuleDomainService h2ModuleDomainService)
        {
            _auditingRepository = auditingRepository;
            _moduleRepository = moduleRepository;
            _h2ModuleDomainService = h2ModuleDomainService;


        }
        /// <summary>
        /// 根据Module获取分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns>科室 初步审核人员 最终审核人员</returns>
        public PagedResultDto<H2ModuleWithAuditingDto> GetPagedH2ModuleWithAuditing(GetPagedH2ModuleWithAuditingInput input)
        {
            var query = _moduleRepository.GetAll()
               .Where(T => T.Module == input.Module)
               .GroupJoin(_auditingRepository.GetAll().Where(T=>T.Module==input.Module),
               x => x.DepartmentId,
               y => y.DepartmentId,
               (x, y) => new { M = x, S = y })
             .SelectMany(x => x.S.DefaultIfEmpty(), (T, S) => new H2ModuleWithAuditingDto
             {
                 Id = S == null ? 0 : S.Id,
                 DepartmentId = T.M.DepartmentId,
                 DepartmentName = T.M.Department.DepartmentName,
                 AuditorId = S.AuditorId,
                 AuditorName = S.Auditor.UserName,
                DoUserName = S.DoUser.UserName,
                DoUserId = S.DoUserId
             })
             .WhereIf(!string.IsNullOrEmpty(input.DepartmentName), T => T.DepartmentName.Contains(input.DepartmentName));

            var queryCount = query.Count();
            var deps = query.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultDto<H2ModuleWithAuditingDto> { Items = deps, TotalCount = queryCount };

        }

        public void SetModuleDoUserWithPermission(H2ModuleWithAuditingDto input,string permission)
        {
            var h2ModuleWithAuditing = input.MapTo<H2ModuleWithAuditing>();
            _h2ModuleDomainService.SetDoUserWithPermission(h2ModuleWithAuditing, permission);
        }

        public void SetModuleAuditorWithPermission(H2ModuleWithAuditingDto input, string permission)
        {
            var h2ModuleWithAuditing = input.MapTo<H2ModuleWithAuditing>();
            _h2ModuleDomainService.SetAuditorWithPermission(h2ModuleWithAuditing, permission);
        }
    }
}
