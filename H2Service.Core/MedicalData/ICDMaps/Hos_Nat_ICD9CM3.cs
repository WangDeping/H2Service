using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.ICDMaps
{
    /// <summary>
    /// 医院与国家手术编码映射(2016~201808)
    /// </summary>
 public   class Hos_Nat_ICD9CM3:Entity
    {
        /// <summary>
        /// 院内手术编码
        /// </summary>
        public string HosCode { get; set; }
        /// <summary>
        /// 国家版手术编码
        /// </summary>
        public string NatCode { get; set; }
        /// <summary>
        /// 国家手术名称
        /// </summary>
        public string NatOperName { get; set; }
        /// <summary>
        /// 手术种类
        /// </summary>
        public string OperKind { get; set; }
        /// <summary>
        /// 手术录入选项
        /// </summary>
        public string Option { get; set; }
    }
}
