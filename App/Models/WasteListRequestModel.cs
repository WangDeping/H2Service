using H2Service.MedicalWastes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models
{
    public class WasteListRequestModel
    {
        public int FlowId { get; set; }

       public MedicalWasteKind Kind { get; set; }
    }
}