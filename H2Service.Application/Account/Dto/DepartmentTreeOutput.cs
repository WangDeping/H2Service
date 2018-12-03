using Abp.AutoMapper;
using H2Service.Authorization;
using H2Service.Authorization.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Account.Dto
{
    
 public   class DepartmentTreeOutput
    {
        public DepartmentTreeOutput()
        {
            state = new TreeState();
        }
        public int id { get; set; }
        public string text { get; set; }

        public string parent { get; set; }

        public TreeState state { get; set; }
    }
}
