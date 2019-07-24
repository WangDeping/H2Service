using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.ICDMaps
{
 public   class InuseICD10:Entity
    {
        public string Code { get; set; }

        public string Name { get; set; }
    }
}
