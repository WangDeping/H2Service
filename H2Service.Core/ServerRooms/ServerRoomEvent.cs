using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms
{
    /// <summary>
    /// 机房事件处理
    /// </summary>
    public class ServerRoomEvent : Entity<int>, ICreationAudited, ISoftDelete
    {
        public SREventType EmergencyType { get; set; }
        public int? ServerEquipmentId { get; set; }
        [ForeignKey("ServerEquipmentId")]
        public virtual ServerEquipment ServerEquipment { get; set; }
        public int? ServerRoomId { get; set; }
        [ForeignKey("ServerRoomId")]
        public virtual ServerRoom ServerRoom { get; set; }
       
        public long? CreatorUserId { get; set; }
        /// <summary>
        /// 处理措施
        /// </summary>
        public string Solutions { get; set; }

        /// <summary>
        /// 问题是否解决
        /// </summary>
        public bool? IsSolved { get; set; }
        /// <summary>
        /// 照片路径
        /// </summary>
        public string ImgsPath { get; set; }
        [ForeignKey("CreatorUserId")]
        public virtual User CreatorUser{get;set;}
        public DateTime CreationTime { get ; set ; }
        public bool IsDeleted { get ; set ; }
    }

    public enum SREventType {
        停电,
        消防安全,
        设备故障,
        机房工程,
        迁入设备,
        迁出设备,
        系统维护,
        来人登记
    }
}
