using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.ICDMaps
{
    /// <summary>
    /// 山东版与国家临床版映射(2018.0902~)
    /// </summary>
 public   class SD_Nat_ICD10:Entity
    {
        /// <summary>
        /// 山东编码
        /// </summary>
        public string SDCode { get; set; }
        /// <summary>
        /// 国家临床2.0 ICD10编码
        /// </summary>
        public string NatCode { get; set; }
        /// <summary>
        ///  国家临床2.0 ICD10名称
        /// </summary>
        public string NatDisName { get; set; }
    }
}
