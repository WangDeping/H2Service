using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization.Departments;
using H2Service.ServerRooms;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Authorization
{
    public class Department : Entity<int>, ISoftDelete
    {
        public Department()
        {
            Permissions =new List<DepartmentPermission>();
        }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 上级部门
        /// </summary>
        public int FatherId { get; set; }
        /// <summary>
        /// 显示顺序
        /// </summary>
        public int Order { get; set; }
        /// <summary>
        /// 部门电话
        /// </summary>
        public string DepartmentPhone{ get; set; }
        /// <summary>
        /// 部门下的人
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        public int? DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public virtual District District { get; set; }

        [ForeignKey("DepartmentId")]
        public virtual ICollection<DepartmentPermission> Permissions { get; set; }

        public virtual ICollection<ServerRoom> ServerRooms { get; set; }

        public bool IsAllowedHasUser { get; set; }
        public bool IsDeleted { get; set ; }
    }
}
