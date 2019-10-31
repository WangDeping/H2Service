using Abp.Application.Services;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.Equipments
{
 public  interface IEquipmentKindTypeAppService: IApplicationService
    {
        /// <summary>
        /// 获取所有设备分类
        /// </summary>
        /// <returns></returns>
        IList<EquipmentTypeDto> GetEqTypeList();
        /// <summary>
        /// 获取所有设备种类
        /// </summary>
        /// <returns></returns>
        IList<EquipmentKindDto> GetEqKindList();
        /// <summary>
        /// 获取所有型号分类
        /// </summary>
        /// <returns></returns>
        IList<EquipmentModelDto> GetEqModelList();
    }
}
