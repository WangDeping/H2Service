using H2Service.MedicalData.DataQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Reports
{
 public   class PatientsReportAppService: IPatientsReportAppService
    {
        private CurrentDataDomainService _currentDataDomainService;


        public PatientsReportAppService(CurrentDataDomainService currentDataDomainService) {

            _currentDataDomainService =currentDataDomainService;
        }
        /// <summary>
        /// 获取当天的在院、出院、入院病人
        /// </summary>
        /// <param name="dep"></param>
        public List<CurrentPatients> GetCurrentPatientsByDep(string dep="") {
            var patsList = _currentDataDomainService.GetCurrentPatientsByDep();
            if (dep != "") {
                return patsList.Where(T => T.Dep == dep).ToList();
            }
            return patsList;
        }
        /// <summary>
        /// 用于Hangfire存储24小时挂号量
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>      
        /// <returns></returns>
        public void StoreOutPatientsQty(DateTime start,DateTime end) {
            _currentDataDomainService.StoreRegistersQty(start,end);  
        }
        /// <summary>
        /// 获取当前的挂号量
        /// </summary>
        /// <param name="dep"></param>
        public List<CurrentRegistersQty> GetCurrentRegsitersQty(string dep = "") {
            var registerQtyList = _currentDataDomainService.GetCurrentRegistersQty();
            if (!string.IsNullOrEmpty(dep))
                return registerQtyList.Where(T => T.Dep == dep).ToList();
            else
                return registerQtyList;
        }
    }
}
