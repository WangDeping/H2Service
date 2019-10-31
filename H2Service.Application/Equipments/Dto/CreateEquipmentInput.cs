using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments.Dto
{
    [AutoMap(typeof(Equipment))]
    public   class CreateEquipmentInput
    {
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
        /// <summary>
        /// 设备分类
        /// </summary>
        public int EquipmentTypeId { get; set; }   
        /// <summary>
        /// 型号
        /// </summary>
        public int EquipmentModelId { get; set; }
        /// <summary>
        /// 所属部门
        /// </summary>
        public int DepartmentId { get; set; }       
          
      
    }
}
