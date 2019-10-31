using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class PagedPatrolLogsResultModel
    {
        public int total { get; set; }
        public  IReadOnlyList<EquipmentPatrolLogDto> rows{ get; set; }
    }
}