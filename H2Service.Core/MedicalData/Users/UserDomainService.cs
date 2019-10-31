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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">住院号</param>
        /// <returns></returns>
        public PatientSample GetPatientSample(string id) {
            var patient = new PatientSample();
            var cmd = DHCWLZBBCommonQuery.GetPatDiagByMedicare(conn);
            cmd.Parameters.Add("InputMedicare", id);
            conn.Open();
            var obj = cmd.ExecuteScalar();           
            var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                patient.Diagnose = reader["admDiag"] + "";
                patient.PatName = reader["papmiName"] + "";
            }
            reader.Close();
            conn.Close();
            return patient;

        }
    }
}
