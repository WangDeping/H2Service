using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentLoanLog))]
    public   class EquipmentLoanLogDto
    {
        public int Id { get; set; }
        /// <summary>
        /// 设备
        /// </summary>
        public int EquipmentId { get; set; }      
        /// <summary>
        /// 借出时间
        /// </summary>
        public DateTime LoanTime { get; set; }
        /// <summary>
        /// 借出人Id
        /// </summary>
        public long LoanUserId { get; set; }
        /// <summary>
        /// 借出人姓名
        /// </summary>
        public string LoanUserName { get; set; }
        /// <summary>
        /// 借出科室
        /// </summary>
        public int LoanDeptId { get; set; }        
        public string LoanDepartmentName { get; set; }
        /// <summary>
        /// 借入人
        /// </summary>
        public long BorrowUserId { get; set; }
       
        public string BorrowUserName { get; set; }
        /// <summary>
        /// 借入科室
        /// </summary>
        public int BorrowDeptId { get; set; }
       
        public string BorrowDeptName { get; set; }
        /// <summary>
        /// 归还时间
        /// </summary>
        public DateTime? ReturnTime { get; set; }
        /// <summary>
        /// 归还人
        /// </summary>
        public long? ReturnUserId { get; set; }
       
        public string ReturnUserName { get; set; }
        /// <summary>
        /// 签收人
        /// </summary>
        public long? SignUserId { get; set; }
      
        public string SignUserName { get; set; }
    }
}
