using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
 public   interface IAuthorizationManager: IDomainService
    {
        //IEnumerable<H2Permission> GetAssignedPermissonByUser(long userId);
        //string GetAssignedPermissonStringByUser(long userId);
    }
}
