using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace App.Models.Equipment
{
    public class PreviewLoanModel
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string UserNumber { get; set; }

        public long? LoanUserId { get; set; }

        public string LoanUserName { get; set; }

        public DateTime LoanTime { get; set; }

        public long? BorrowUserId { get; set; }

        public string BorrowUserName { get; set; }

        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public int BorrowDepId { get; set; }

        public string BorrowDepName { get; set; }

        public int LoanDepId { get; set; }

        public string LoanDepName { get; set; }

        public string QrCode { get; set; }

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