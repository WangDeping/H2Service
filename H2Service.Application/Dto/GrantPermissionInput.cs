using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Dto
{
public    class GrantPermissionInput
    {
        public GrantPermissionInput()
        {
            PermissionNames = new List<string>();

        }
        /// <summary>
        /// 用来接收权限树选中的节点
        /// </summary>
        public List<string> PermissionNames { get; set; }

        /// <summary>
        /// 真正要赋予的权限节点
        /// </summary>
        public List<string> Permissions { get {
                var splitList = new List<string>();
                foreach (var permissionName in PermissionNames)//把每个权限用"."拆分成从上级权限到叶子节点权限
                {
                    var deep = permissionName.Split('.');
                    string instPermission = "";
                    for (var level = 0; level < deep.Count(); level++)
                    {
                        instPermission += deep[level] + ".";
                        splitList.Add(instPermission.Substring(0, instPermission.Length - 1));
                    }
                }
                return splitList.Distinct().ToList();

            } }

        public long? UserId { get; set; }

        public int? DepartmentId { get; set; }

        public int? RoleId { get; set; }

        
    }
}
