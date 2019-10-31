using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace H2Service.Web.Models.Report
{
    public class CurrentPatientsCounts
    {
        public int NewPats { get; set; }

        public int InPats { get; set; }

        public int OutPats { get; set; }

        public List<string> Deps { get; set; }

        public List<int> DepInpats { get; set; }
    }

    public class CurrentPatientsCountsDetail {
        public string Dep { get; set; }
        public int Counts { get; set; }
    }
}