using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
 public  interface IMedicalWasteDomainService:IDomainService
    {
        MedicalWasteFlow InitOrGetDefaultFlow(int departmentId);

        void Delivery(int districtId, string summary);
    }
}
