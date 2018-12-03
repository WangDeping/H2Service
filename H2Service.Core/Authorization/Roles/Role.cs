using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
    public class Role : Entity,ISoftDelete
    {
        public string RoleName { get; set; }
        [ForeignKey("RoleId")]
        public virtual ICollection<RolePermission> Permissions { get; set; }  //基于角色的权限列表
        public virtual ICollection<User> Users { get; set; }
        public long? CreatorUserId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
