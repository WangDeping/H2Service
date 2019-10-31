using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class CreateUsageModel
    {
        public int EquipmentId { get; set; }       
        public string PatientAdmNo { get; set; }  
        public DateTime? BeginTime { get; set; }
       
    }
}