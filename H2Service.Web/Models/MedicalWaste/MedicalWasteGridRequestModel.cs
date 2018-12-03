using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.MedicalWaste
{
    public class MedicalWasteGridRequestModel:PagedInputDto
    {
        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public int DepartmentId { get; set; }
    }
}