using InterSystems.Data.CacheClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.MedicalData
{
 public   class IntersystemsCache
    {
        public static CacheConnection GetConnection() {

            return new CacheConnection(WebConfigurationManager.ConnectionStrings["Intersystem"].ConnectionString);

        }
    }
}
