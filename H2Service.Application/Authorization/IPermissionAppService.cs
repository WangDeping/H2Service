using Abp.Application.Services;
using Abp.Authorization;
using H2Service.Authorization.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
 public   interface IPermissionAppService: IApplicationService
    {
       
        List<PermissionTreeOutput> PermissionTreeList { get;  }

         Permission GetPermission(string name);

        List<PermissionTreeOutput> GetAssignedPermissionTree(AssignPermissionInput input);

       
    }
}
