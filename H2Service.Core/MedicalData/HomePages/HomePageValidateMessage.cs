using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.HomePages
{
 public   class HomePageValidateMessage:Entity,ISoftDelete
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
        public DateTime? DischargeDate { get; set; }

        /// <summary>
        /// 验证类型
        /// </summary>
        public ValidateType ValidateType { get; set; }
        /// <summary>
        /// 发送人
        /// </summary>
        public string SendUser { get; set; }
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 校验状态(住院总质控)
        /// </summary>
        public ValidateStatus ValidateStatus { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get ; set ; }
    }


    public enum ValidateType {
        全部=0,
        首页填写=1,//首页填写校验
        手工质控=2,//手工质控校验
        出院日期一致性=3,//出院日期一致性校验
        住院总质控=4,
        临床改正反馈=5
    }

    public enum ValidateStatus {
        无状态=0,
        问题通知=1,
        修正反馈=2,    
        病案上架=80,
        完成=99
        

    }
}
