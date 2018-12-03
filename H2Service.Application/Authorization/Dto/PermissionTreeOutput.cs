using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization.Dto
{
 public   class PermissionTreeOutput
    {
        public PermissionTreeOutput()
        {
            state = new TreeState();
        }
        public string id { get; set; }
        public string text { get; set; }

        public string parent { get; set; }

        public TreeState state { get; set; }
    }
    public class TreeState
    {
        public TreeState()
        {
            opened = "true";
            selected = false;
        }
        public string opened { get; set; }
        public bool selected { get; set; }
    }
}
