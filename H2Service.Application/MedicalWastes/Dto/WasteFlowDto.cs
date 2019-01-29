using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes.Dto
{
    [AutoMap(typeof(MedicalWasteFlow))]
    public    class WasteFlowDto
    {
        public int Id { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string NurseName { get; set; }

        public string CollectUserName { get; set; }

        public string Status { get; set; }

        public string CollectTime { get; set; }

        public int? MedicalWasteDeliveryId { get; set; }

    }
}
