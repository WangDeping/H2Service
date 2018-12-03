using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
  public  class H2Permission:Entity
    {        
        public string PermissionName { get; set; }
    }
}
