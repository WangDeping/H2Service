using Abp.Dependency;
using Abp.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Extensions
{
 public   interface IAbpSessionExtension:IAbpSession
    {
        string UserName { get;}

        string UserNumber { get; }
    }
}
