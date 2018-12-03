using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using H2Service.Dto;
using H2Service.Salaries.Dto;
using H2Service.Salarires;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Salaries
{
 public   interface  ISalaryAppService:IApplicationService
    {
        void UploadSalary(CreateSalaryPeriodInput period);

        
        IEnumerable<SalaryType> GetAllTypes();

        PagedResultDto<SalaryPeriodDto> GetPagedSalaryPeriods(PagedInputDto input);

        /// <summary>
        /// 根据工号获取个人工资明细
        /// </summary>
        /// <param name="UserNumber"></param>
        /// <returns></returns>
       
        PersonalSalaryOutput GetPersonSalary(PersonalSalaryInput input);

        /// <summary>
        /// 个人工资历史
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        PagedResultDto<SalaryDetailDto> GetPagedPersonalSalaryPeriods(PagedInputDto input);

        /// <summary>
        /// 根据Id查看工资明细
        /// </summary>
        /// <param name="detailId"></param>
        /// <returns></returns>
        PersonalSalaryOutput GetPersonSalary(int detailId);
    }
}
