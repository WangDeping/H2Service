using Abp.Domain.Services;
using InterSystems.Data.CacheClient;
using LanSeCheng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.Users
{
 public   class UserDomainService:DomainService
    {
        private CacheConnection conn;
        public UserDomainService() {
            conn = IntersystemsCache.GetConnection();

        }
        /// <summary>
        /// 根据Id获取工号
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetUserById(string id) {
            var userNumber = "";
            var cmd = DHCExportService.GetUserNoByUserId(conn);
            cmd.Parameters.Add("InputUserId",id);
            conn.Open();          
           var obj= cmd.ExecuteScalar();
            if (obj != null)
                userNumber = obj.ToString();
            conn.Close();
            return userNumber;

        }
    }
}
