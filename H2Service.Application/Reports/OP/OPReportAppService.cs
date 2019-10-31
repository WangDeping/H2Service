using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using AutoMapper.QueryableExtensions;
using Castle.Core.Logging;
using H2Service.Dto;
using H2Service.MedicalData.OPMedicalDiagnose;
using H2Service.Reports.Dto;
using Microsoft.Owin.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace H2Service.Reports
{
    /// <summary>
    /// 门诊报表
    /// </summary>
    public   class OPReportAppService: ApplicationService,IOPReportAppService
    {
        /// <summary>
        /// 根据诊断获取就诊人数
        /// </summary>
        private readonly IRepository<OPMedicalDiagnose> _OPMedicalDiagnoseRepository;

        private readonly List<string> filtterDeps = new List<string> { "儿童保健中心", "药剂配送科", "超声2科东院区", "社会卫生科", "高压氧门诊", "查体中心", "病历复印", "复印病历" };
        /// <summary>
        /// 构造子
        /// </summary>
        /// <param name="OPMedicalDiagnoseRepository"></param>
        public OPReportAppService(IRepository<OPMedicalDiagnose> OPMedicalDiagnoseRepository) {
            _OPMedicalDiagnoseRepository = OPMedicalDiagnoseRepository;
        }
        /// <summary>
        /// 统计实际的就诊人次
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public PagedResultWithSumDto<GetOutPatientsQtyByDepOutput> GetOutPatientsQty(GetOutPatientsQtyByDepInput input) {         
            var query = _OPMedicalDiagnoseRepository.GetAll()
               // .WhereIf(!string.IsNullOrEmpty(input.Dep), T => T.AdmDep == input.Dep)
                .Where(T => T.AdDate >= input.Start && T.AdDate < input.End)
                .GroupBy(T=>T.AdmDep)
                .Select(M=>new GetOutPatientsQtyByDepOutput {   Dep=M.Key, Qty=M.Count()}).ToList();
            var result = query.Where(T => FiltterDep(T.Dep)).OrderBy(T=>T.Dep).ToList();
            var queryCount = result.Count();
            var sum = result.Sum(T => T.Qty);
            var items= result.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            return new PagedResultWithSumDto<GetOutPatientsQtyByDepOutput> { Items =items, TotalCount = queryCount, SumQty=sum };
        }


        /// <summary>
        /// 根据不同区间类型获取数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<GetOutPatientsInPeriodOutput> GetOutPatientsInPeriod(GetOutPatientsInPeriodInput input) {
            Logger.Error("开始查询"+DateTime.Now.ToString());
            var allPats = _OPMedicalDiagnoseRepository.GetAll().Where(T=>T.AdDate.Value.Year==2019).MapTo<IList<OPQtyMonthDto>>();//.Where(T=>FiltterDep(T.AdmDep))
            Logger.Error("过滤" + DateTime.Now.ToString());
            var output = allPats                
                .GroupBy(G => G.Month)
                .Select(S => new GetOutPatientsInPeriodOutput { Date = S.Key, Qty = S.Count() });
            Logger.Error("完成" + DateTime.Now.ToString());

            return output.ToList();
        }
        
        
        
        
        
        
        
        
        
        
        
        
        
        /// <summary>
        /// 过滤无效就诊科室
        /// </summary>
        /// <param name="dep"></param>
        /// <returns></returns>
        private bool FiltterDep(string dep) {
           
            return  !filtterDeps.Contains(dep);
        }

      
    }
}
