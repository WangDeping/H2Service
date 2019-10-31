using Abp.Domain.Services;
using Abp.Runtime.Caching;
using InterSystems.Data.CacheClient;
using LanSeCheng;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.MedicalData.DataQuery
{
 public   class CurrentDataDomainService: DomainService
    {
        private CacheConnection conn;
        private readonly ICacheManager _cacheManager;

        public CurrentDataDomainService(ICacheManager cacheManager) {
            _cacheManager = cacheManager;
        }
        /// <summary>
        /// 获取当前的出入院在院人数
        /// </summary>
        public List<CurrentPatients> GetCurrentPatientsByDep() {
            var patList = new List<CurrentPatients>();
            conn = IntersystemsCache.GetConnection();
            var cmd =DHCExportService.LocMRIPLoad(conn);
            conn.Open();
            var reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    var pat = new CurrentPatients
                    {
                        Dep = reader["Loc"] + "",
                        NewPats = (int)reader["LocRY"],
                        OutPats = (int)reader["LocCY"],
                        InPats = (int)reader["LocZY"]
                    };
                    patList.Add(pat);
                }
                return patList;
            }
            catch(Exception ex)
            {            
                Logger.Error(ex.Message);
                return patList;
            }
            finally {
                conn.Close();
            }
        }
        /// <summary>
        /// 缓存门诊人数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public void StoreRegistersQty(DateTime start,DateTime end) {

            var patList = new List<CurrentRegistersQty>();
            conn = IntersystemsCache.GetConnection();
            var cmd = DHCWLZBBCommonQuery.LocRegPatientQty(conn);
            cmd.Parameters.Add("startDate",start.ToString("yyyy-MM-dd"));
            cmd.Parameters.Add("startTime",start.ToShortTimeString());
            cmd.Parameters.Add("endDate",end.ToString("yyyy-MM-dd"));
            cmd.Parameters.Add("endTime",end.ToShortTimeString());
            var currentHour = end.Hour.ToString().PadLeft(2, '0');
            conn.Open();
            var reader = cmd.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    var pat = new CurrentRegistersQty
                    {
                        Dep = reader["LocDesc"] + "",
                        CurrentQty=(int)reader["totalRegRC"],
                        Hour=currentHour
                    };
                    patList.Add(pat);
                   
                }
                _cacheManager.GetCache("RegistersQty").Set(currentHour, patList);
                // return patList;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
               // return patList;
            }
            finally
            {
                conn.Close();
            }
        }
        /// <summary>
        /// 获取缓存的门诊挂号数(24小时)
        /// </summary>
        /// <returns></returns>
        public List<CurrentRegistersQty> GetCurrentRegistersQty() {
            var keys = new string[24];
            for (var i = 0; i < 24; i++) {
                keys[i] = i.ToString().PadLeft(2, '0');
            }           
            var cache24Qty=  _cacheManager.GetCache("RegistersQty").Get(keys, () => {
                return new List<CurrentRegistersQty>();
            });
            var result = new List<CurrentRegistersQty>();
            foreach (var list in cache24Qty) {
              result= result.Concat(list).ToList();
            }
            return result;
        }
    }
    /// <summary>
    /// 出、入、住院患者
    /// </summary>
    public class CurrentPatients {
        public string Dep { get; set; }

        public int NewPats { get; set; }

        public int OutPats { get; set; }

        public int InPats { get; set; }

    }
    /// <summary>
    /// 门诊患者
    /// </summary>
    public class CurrentRegistersQty
    {
        public string Dep { get; set; }

        public int CurrentQty { get; set; }

        public string Hour { get; set; }
    }
}
