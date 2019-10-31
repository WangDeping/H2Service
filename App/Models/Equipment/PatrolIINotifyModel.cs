using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class PatrolIINotifyModel
    {
        public IEnumerable<EquipmentOutput> NotPatrol { get; set; }

        public IEnumerable<EquipmentOutput> HasPatrol { get; set; }

        public IEnumerable<EquipmentOutput> NotWork { get; set; }
    }
}