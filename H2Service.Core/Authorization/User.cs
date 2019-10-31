using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities.Auditing;
using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace H2Service.Authorization
{
 public   class User:Entity<long>,IFullAudited
    {
        public User()
        {
            Permissions = new List<UserPermission>();
        }
        
        [Required]
        public string UserName { get; set; }

        public string Code { get; set; }

        [Required]
        public string UserNumber { get; set; }
        [Required]
        public string Password { get; set; }
        public string AvatarUrl { get; set; }
        [Required]
        public string TelPhone { get; set; }
        public string TelPhone2 { get; set; }          
        public Gender Gender { get; set; }
        public string Brithday { get; set; }
        public string Email { get; set; }
        public long? CreatorUserId { get ; set ; }
        public DateTime CreationTime { get ; set ; }
        public long? LastModifierUserId { get ; set ; }
        public DateTime? LastModificationTime { get ; set ; }
        public long? DeleterUserId { get ; set ; }
        public DateTime? DeletionTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        [ForeignKey("UserId")]
        public virtual ICollection<UserPermission> Permissions { get; set; }
    }

    public enum Gender
    {
        UnKnown=0,
        Male=1,
        Female=2

    }
}
