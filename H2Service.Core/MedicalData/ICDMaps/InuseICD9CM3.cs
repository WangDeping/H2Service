using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.ICDMaps
{
 public   class  InuseICD9CM3: Entity
    {
        public string OperCode { get; set; }

        public string OperName { get; set; }

        public string OperDegree { get; set; }

        public string OperKind { get; set; }
        
        public string Option { get; set; }
      
    }
}
