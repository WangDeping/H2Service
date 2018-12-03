using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalWastes
{
 public   class ExternalCompany:Entity
    {
        public string CompanyName { get; set; }
       
        public Aspect Aspect { get; set; }
        public virtual  ICollection<ExternalUser> Users { get; set; }
    }
    public enum Aspect
    {
        医疗废物


    }
}
