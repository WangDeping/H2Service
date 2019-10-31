using H2Service.Account.Dto;
using H2Service.Equipments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace App.Models.Equipment
{
    public class EquipmentIndexModel
    {
        public IReadOnlyList<EquipmentOutput> EquipmentGrids { get; set; }
        public IList<DepartmentModulesRelationDto> RelatedDepts { get; set; }
    }
}