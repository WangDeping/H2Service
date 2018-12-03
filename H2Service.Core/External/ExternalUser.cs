using Abp.Domain.Entities;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
 public   class ExternalUser:Entity,ISoftDelete
    {
        public string UserName { get; set; }

        /// <summary>
        /// 唯一编码用于标识用户条码/二维码
        /// </summary>
        public string Code { get; set; }

        public string TelPhone { get; set; }

        public string IDNumber { get; set; }

        public string AvatarUrl { get; set; }

        public Gender Gender { get; set; }

        [ForeignKey("ExternalCompanyId")]
        public virtual ExternalCompany Company { get; set; }

        public int ExternalCompanyId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
