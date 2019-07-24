using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.ICDMaps
{
    /// <summary>
    /// 医院与国家2.0疾病编码映射(2016~2018.08)
    /// </summary>
 public   class Hos_Nat_ICD10: Entity
    {
        /// <summary>
        /// 院内编码
        /// </summary>
        public string HosCode { get; set; }
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
