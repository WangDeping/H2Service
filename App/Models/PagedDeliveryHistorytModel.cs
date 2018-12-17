using H2Service.MedicalWastes.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class PagedDeliveryHistoryRequestModel
    {
        public int total { get; set; }
        public IReadOnlyList<WasteDeliveryDto> rows{ get; set; }
    }
}