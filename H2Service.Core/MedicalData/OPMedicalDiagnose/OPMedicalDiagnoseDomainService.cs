using Abp.Domain.Repositories;
using Abp.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterSystems.Data.CacheClient;
using LanSeCheng;

namespace H2Service.MedicalData.OPMedicalDiagnose
{
 public   class OPMedicalDiagnoseDomainService : DomainService
    {
        private readonly IRepository<OPMedicalDiagnose> _OPMedicalDiagnoseRepository;
        private CacheConnection conn;      
        public OPMedicalDiagnoseDomainService(IRepository<OPMedicalDiagnose> OPMedicalRecordRepository) {
            _OPMedicalDiagnoseRepository = OPMedicalRecordRepository;
            conn = IntersystemsCache.GetConnection();
        }
        /// <summary>
        /// 同步门诊就诊信息
        /// </summary>
        /// <param name="dateFrom">开始</param>
        /// <param name="dateTo">结束</param>
        public void SynchronousOPMedicalDiagnose(string dateFrom, string dateTo) {
            try
            {
                var cmd = DHCExportService.OPPatDiagnos(conn);
                cmd.Parameters.Add("startDate",dateFrom);
                cmd.Parameters.Add("endDate",dateTo);
                conn.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read()) {
                    var admNo = reader["admNo"]+"";
                    var record = _OPMedicalDiagnoseRepository.FirstOrDefault(T => T.AdmNo == admNo);
                    record = record == null ? new OPMedicalDiagnose() : record;
                    record.AdmNo = admNo;
                    record.PatNo = reader["papmiNo"] + "";
                    record.PatName = reader["papmiName"] + "";
                    record.AdDate = ToDateTime(reader["admDateTime"]);
                    record.Birthday = ToDateTime(reader["borthDay"]);
                    record.Age =ToInt32(reader["age"]);
                    record.PhoneNumber = reader["phone"] + "";
                    record.Address = reader["address"] + "";
                    record.AdmDep = reader["admLocDesc"] + "";
                    record.DoctorNo = reader["admDocCode"] + "";
                    record.DoctorName = reader["admDocName"] + "";
                    record.DiagnoseCode1 = reader["MZZDJBDM1"] + "";
                    record.DiagnoseName1 = reader["MZZDJBMC1"] + "";
                    record.DiagnoseType1= reader["MZZDTYPE1"] + "";
                    record.DiagnoseStatus1 = reader["MZZDSTATUS1"] + "";
                    record.DiagnoseCode2 = reader["MZZDJBDM2"] + "";
                    record.DiagnoseName2 = reader["MZZDJBMC2"] + "";
                    record.DiagnoseType2 = reader["MZZDTYPE2"] + "";
                    record.DiagnoseStatus2 = reader["MZZDSTATUS2"] + "";
                    record.DiagnoseCode3= reader["MZZDJBDM3"] + "";
                    record.DiagnoseName3 = reader["MZZDJBMC3"] + "";
                    record.DiagnoseType3 = reader["MZZDTYPE3"] + "";
                    record.DiagnoseStatus3 = reader["MZZDSTATUS3"] + "";
                    record.DiagnoseCode4 = reader["MZZDJBDM4"] + "";
                    record.DiagnoseName4 = reader["MZZDJBMC4"] + "";
                    record.DiagnoseType4 = reader["MZZDTYPE4"] + "";
                    record.DiagnoseStatus4 = reader["MZZDSTATUS4"] + "";
                    record.DiagnoseCode5 = reader["MZZDJBDM5"] + "";
                    record.DiagnoseName5 = reader["MZZDJBMC5"] + "";
                    record.DiagnoseType5 = reader["MZZDTYPE5"] + "";
                    record.DiagnoseStatus5 = reader["MZZDSTATUS5"] + "";                  
                    _OPMedicalDiagnoseRepository.InsertOrUpdate(record);
                }
                reader.Close();
            }

            catch (Exception ex) {
                Logger.Error("同步门诊诊断出错:" + ex.Message + "日期为:" + dateFrom );
            }
            finally
            {
                conn.Close();
            }

        }

        private DateTime? ToDateTime(object obj)
        {
            string dateString = obj.ToString();
            if (string.IsNullOrEmpty(dateString))
                return null;
            else
                return DateTime.Parse(dateString);
        }

        private int? ToInt32(object obj)
        {
            try
            {
                if (string.IsNullOrEmpty(obj.ToString()))
                    return null;
                else
                    return Convert.ToInt32(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}
