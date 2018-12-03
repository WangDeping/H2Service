using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.QC.Dto
{
    [AutoMap(typeof(QCAppraisalPeriod))]
    public  class QCAppraisalPeriodDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Period { get; set; }

        public string BeginDate { get; set; }

        public string EndDate { get; set; }
       
        public string CreatorUserName { get; set; }

        public string CreatorDepartmentName { get; set; }

        public string CreationTime { get; set; }

        public string Status { get; set; }

    }
}
