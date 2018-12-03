using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Authorization;
using H2Service.Authorization.Permissions;
using H2Service.Authorization.Dto;
using Abp.Domain.Repositories;

namespace H2Service.Authorization
{
    public class PermissionAppService : H2ServiceAppServiceBase, IPermissionAppService
    {
        private readonly IPermissionDomainService _permissionDomainService;
      
        public List<PermissionTreeOutput> PermissionTreeList { get; }

        public PermissionAppService(IPermissionDomainService permissionDomainService         )
        {
            _permissionDomainService = permissionDomainService;
            
            var root = GetPermission(null);//权限根Pages
            PermissionTreeList = new List<PermissionTreeOutput>();
            PermissionTreeList.Add(new PermissionTreeOutput { id = root.Name, text =root.Name, parent = "#" });
            this.GetPermissionTree();


        }
        /// <summary>
        /// 获取部门/角色/用户已分配的权限
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public List<PermissionTreeOutput> GetAssignedPermissionTree(AssignPermissionInput input)
        {
            IEnumerable<H2Permission> permissions=new List<H2Permission>();
            if (input.UserId != null)
                permissions = _permissionDomainService.GetPermissionByUser((long)input.UserId);
            else if (input.RoleId != null)
             permissions = _permissionDomainService.GetPermissionByRole((int)input.RoleId); 
            else if (input.DepartmentId != null)
                permissions = _permissionDomainService.GetPermissionByDepartment((int)input.DepartmentId);
            var ret_List = new List<PermissionTreeOutput>(this.PermissionTreeList);
            Logger.Error("input.RoleId:"+ input.RoleId + "ret_List列表的数量" +ret_List.Count.ToString());
            foreach (var p in permissions)
            {
                if (ret_List.Any(P =>P.parent==p.PermissionName))
                    continue;
                Logger.Error("权限名称:" +p.PermissionName);
                var update_permission = ret_List.First(T=>T.id==p.PermissionName);
                var remove_index=ret_List.IndexOf(update_permission);
                ret_List.Remove(update_permission);
                update_permission.state.selected =true;
                ret_List.Insert(remove_index,update_permission);
            }
            return ret_List;
        }
        /// <summary>
        /// 递归调用所有权限树
        /// </summary>
        /// <param name="name"></param>
        private void GetPermissionTree(string name=null)
        {           
            var root_permission = _permissionDomainService.GetPermission(name);
           
            foreach (var child in root_permission.Children)
            {
                string tempText = child.DisplayName.ToString();
                tempText = tempText.Substring(tempText.IndexOf(":") + 1);
                tempText = tempText.Substring(0, tempText.IndexOf(","));
                PermissionTreeList.Add(new PermissionTreeOutput
                {
                    id = child.Name,
                    text =tempText,
                    parent = root_permission.Name
                });
                GetPermissionTree(child.Name);
            }
           
              
          
        }
        public Permission GetPermission(string name)
        {
            return _permissionDomainService.GetPermission(name);

        }


       
    }
}
