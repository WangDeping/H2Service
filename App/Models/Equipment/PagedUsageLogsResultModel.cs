using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class PagedUsageLogsResultModel
    {
        public int total { get; set; }
        public IReadOnlyList<EquipmentUsageLogDto> rows { get; set; }
    }
}