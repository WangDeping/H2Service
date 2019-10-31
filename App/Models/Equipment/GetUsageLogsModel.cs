using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class GetUsageLogsModel: PagedInputDto
    {
        public int EquipmentId { get; set; }
    }
}