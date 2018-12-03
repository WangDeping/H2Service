using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
 public   class Select2UsersInDepartmetInput
    {
        public int departmentId { get; set; }

        public string userName { get; set; }

        public int rows { get; set; }

        public int page { get; set; }
    }
}
