using Abp.Application.Services;
using H2Service.MedicalData.DataQuery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Reports
{
 public  interface IPatientsReportAppService:IApplicationService
    {
        List<CurrentPatients> GetCurrentPatientsByDep(string dep = "");

        List<CurrentRegistersQty> GetCurrentRegsitersQty(string dep = "");
    }
}
