using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Report
{
    public class CurrentRegistersQtyModel
    {
        public int Total { get; set; }

        public List<string> HourList { get; set; }

        public List<int> QtyList { get; set; }
     
    }

    public class CurrentRegistersQtyDetailModel {
        public int Qty { get; set; }

        public string Hour { get; set; }

    }
}