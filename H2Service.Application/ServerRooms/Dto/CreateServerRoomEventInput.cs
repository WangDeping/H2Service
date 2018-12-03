using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.ServerRooms.Dto
{
    [AutoMap(typeof(ServerRoomEvent))]
    public   class CreateServerRoomEventInput
    {
        public int Id { get; set; }
        public SREventType EmergencyType { get; set; }
        public int? ServerEquipmentId { get; set; }
        public int? ServerRoomId { get; set; }
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

        public DateTime CreationTime { get; set; }
    }
}
