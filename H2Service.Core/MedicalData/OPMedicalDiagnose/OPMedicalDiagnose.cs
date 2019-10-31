using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.OPMedicalDiagnose
{
 public   class OPMedicalDiagnose : Entity
    {
        public string AdmNo { get; set; }

        public string PatNo { get; set; }

        public DateTime? AdDate { get; set; }

        public string PatName { get; set; }

        public DateTime? Birthday { get; set; }
        public int? Age { get; set; } 

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public string AdmDep { get; set; }

        public string DoctorNo { get; set; }

        public string DoctorName { get; set; }

        public string DiagnoseCode1 { get; set; }

        public string DiagnoseName1 { get; set; }

        public string DiagnoseType1 { get; set; }

        public string DiagnoseStatus1 { get; set;}

        public string DiagnoseCode2 { get; set; }

        public string DiagnoseName2 { get; set; }

        public string DiagnoseType2{ get; set; }

        public string DiagnoseStatus2 { get; set; }

        public string DiagnoseCode3 { get; set; }

        public string DiagnoseName3 { get; set; }

        public string DiagnoseType3 { get; set; }

        public string DiagnoseStatus3 { get; set; }

        public string DiagnoseCode4 { get; set; }

        public string DiagnoseName4 { get; set; }

        public string DiagnoseType4 { get; set; }

        public string DiagnoseStatus4 { get; set; }

        public string DiagnoseCode5 { get; set; }

        public string DiagnoseName5 { get; set; }

        public string DiagnoseType5 { get; set; }

        public string DiagnoseStatus5 { get; set; }
   

    }
}
