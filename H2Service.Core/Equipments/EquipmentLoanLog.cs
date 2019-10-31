using Abp.Domain.Entities;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
    /// <summary>
    /// 设备借出管理
    /// </summary>
    public class EquipmentLoanLog : Entity
    {
        /// <summary>
        /// 设备
        /// </summary>
        public int EquipmentId { get; set; }
        [ForeignKey("EquipmentId")]
        public virtual Equipment Equipment { get; set; }
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 借出人
        /// </summary>
        public long LoanUserId { get; set; }
        [ForeignKey("LoanUserId")]
        public virtual User LoanUser { get; set; }
        /// <summary>
        /// 借出科室
        /// </summary>
        public int LoanDeptId { get; set; }
        [ForeignKey("LoanDeptId")]
        public virtual Department LoanDepartment { get; set; }
        /// <summary>
        /// 借入人
        /// </summary>
        public long BorrowUserId { get; set; }
        [ForeignKey("BorrowUserId")]
        public virtual User BorrowUser { get; set; }
        /// <summary>
        /// 借入科室
        /// </summary>
        public int BorrowDeptId { get; set; }
        [ForeignKey("BorrowDeptId")]
        public virtual Department BorrowDepartment { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }
        /// <summary>
        /// 归还人
        /// </summary>
        public long? ReturnUserId { get; set; }
        [ForeignKey("ReturnUserId")]
        public virtual User ReturnUser { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public long? SignUserId { get; set; }
        [ForeignKey("SignUserId")]
        public virtual User SignUser { get; set; }
    }
}
