using H2Service.Equipments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class CreatePatrolModel
    {
        public string Summary { get; set; }

        public int EquipmentId { get; set; }

        public bool MainCheck { get; set; }

        public Dictionary<int, string> Details { get; set; }
    }
}