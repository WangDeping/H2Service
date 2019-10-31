using Abp.AutoMapper;
using H2Service.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(Equipment))]
    public   class EquipmentOutput
    {
        public EquipmentOutput() {

            Properties = new List<EquipmentPropertyDto>();
           
        }
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 设备种类
        /// </summary>
        public int EquipmentKindId { get; set; }
        public string EquipmentKindName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public int EquipmentTypeId { get; set; }
        public string EquipmentTypeName { get; set; }
        public string EquipmentTypeIcon { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        /// <summary>
        /// 设备型号
        /// </summary>
        public int EquipmentModelId { get; set; }
        public string EquipmentModelName { get; set; }

        public string Status { get; set; }
        public IList<EquipmentPropertyDto> Properties { get; set; }    
        /// <summary>
        /// 是否正在使用,True在用
        /// </summary>
        public bool IsUsing { get; set; }
        /// <summary>
        /// 是否外借,值为0为未外借，否则是借用Id
        /// </summary>
        public int? IsLoaning_Id { get; set; }
        /// <summary>
        /// 是否I级巡视完毕
        /// </summary>
        public bool HasPatrol_I { get; set; }
        /// <summary>
        /// 是否II巡视完成
        /// </summary>
        public bool HasPatrol_II { get; set; }
    }
}
