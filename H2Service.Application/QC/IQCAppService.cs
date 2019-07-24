using Abp.Application.Services;
using Abp.Application.Services.Dto;
using H2Service.Dto;
using H2Service.QC.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.QC
{
 public   interface IQCAppService:IApplicationService
    {
        PagedResultDto<QCAppraisalPeriodDto> GetPagedQCAppraisalPeriod(PagedInputDto input);

        void CreatePeriod(QCAppraisalPeriodDto input);

        void UpdatePeriod(QCAppraisalPeriodDto input);

        QCAppraisalPeriodDto GetPeriod(int Id);

        /// <summary>
        /// 查看某一职能科室扣分
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        IEnumerable<QCDetailDto> GetDetailsByPeriod(int Id);

        void CreateDetail(QCDetailDto input);

        void RemoveDetail(int Id);

        void Test();

    }
}
