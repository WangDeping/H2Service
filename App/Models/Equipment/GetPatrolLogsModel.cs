using H2Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class GetPatrolLogsModel: PagedInputDto
    {
        public string Code { get; set; }

       
    }
}