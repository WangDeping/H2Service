using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(EquipmentLoanLog))]
    public    class LoanEquipmentInput
    {
     
        public int EquipmentId { get; set; }
       
        
        public DateTime LoanTime { get; set; }
       
        public long LoanUserId { get; set; }      
       
        
        public int LoanDeptId { get; set; }
       
       
        public long BorrowUserId { get; set; }
       
        
        public int BorrowDeptId { get; set; }
       
       
    }
}
