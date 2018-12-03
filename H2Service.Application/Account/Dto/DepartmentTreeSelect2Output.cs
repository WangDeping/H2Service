using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
 public   class DepartmentTreeSelect2Output
    {
        public DepartmentTreeSelect2Output()
        {
            children = new List<DepartmentTreeSelect2Output>();
        }
        public int id { get; set; }

        public string text { get; set; }

        public List<DepartmentTreeSelect2Output> children { get; set; }
    }
}
