using Abp.AutoMapper;
using H2Service.MedicalData.HomePages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.HomePages.Dto
{
    [AutoMap(typeof(HomePageValidateMessage))]
    public class HomePageValidateMessageDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id{ get; set; }
        /// <summary>
        /// 工号
        /// </summary>
        public string UserNumber { get; set; }
        /// <summary>
        /// 病案号
        /// </summary>
        public string BAH { get; set; }
        /// <summary>
        /// 就诊记录
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
        public string ValidateType { get; set; }
        /// <summary>
        /// 验证状态
        /// </summary>
        public string ValidateStatus { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public string SendTime { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string SendUser { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
