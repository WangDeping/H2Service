using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.QC.Dto
{
    [AutoMap(typeof(QCAppraisalDetail))]
    public   class QCDetailDto
    {
        public int Id { get; set; }

        public int DepartmentPunishmentPeriodId { get; set; }

        public int FunctionalDepartmentId { get; set; }

        public string FunctionalDepartmentName { get; set; }

        public int PunishedDepartmentId { get; set; }

        public string PunishedDepartmentName { get; set; }

        public long? PunishedUserId { get; set; }

        public string PunishedUserName { get; set; }

        public decimal Points { get; set; }

        public long? CreatorUserId { get; set; }

        public string CreatorUserName { get; set; }

        public string Desc { get; set; }
    }
}
