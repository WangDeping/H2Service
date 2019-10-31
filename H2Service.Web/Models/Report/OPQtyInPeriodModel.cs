using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Report
{
    public class OPQtyInPeriodModel
    {
        public List<string> PeriodList { get; set; }

        public List<int> QtyList { get; set; }
    }
}