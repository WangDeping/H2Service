using H2Service.MedicalData.OPMedicalDiagnose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.OPDiagnose
{
 public   class OPDiagnoseAppService:H2ServiceAppServiceBase,IOPDiagnoseAppService
    {
        private readonly OPMedicalDiagnoseDomainService _OPMedicalDiagnoseDomainService;

        public OPDiagnoseAppService(OPMedicalDiagnoseDomainService OPMedicalDiagnoseDomainService) {
            _OPMedicalDiagnoseDomainService = OPMedicalDiagnoseDomainService;

        }

        public void SynchronousDiagnose(string dateFrom,string dateTo) {
          
            _OPMedicalDiagnoseDomainService.SynchronousOPMedicalDiagnose(dateFrom,dateTo);

        }
    }
}
