using H2Service.MedicalData.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.HomePages.Dto
{
    /// <summary>
    /// 住院总质控病历
    /// </summary>
 public   class QCValidateInput
    {
        /// <summary>
        /// 接收人工号
        /// </summary>
        public string UserNumber { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        public string BAH { get; set; }
        /// <summary>
        /// 就诊记录ID
        /// </summary>
        public string AdmNo { get; set; }
        /// <summary>
        /// 出院科室
        /// </summary>
        public string Dep { get; set; }
        /// <summary>
        /// 验证信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 出院日期
        /// </summary>
        public string DischargeDate { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>       
        /// <summary>
        /// 发送人工号
        /// </summary>
        public string SendUser { get; set; }
        
    }
}
