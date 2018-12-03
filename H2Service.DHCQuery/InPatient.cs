using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.DHCQuery
{
 public   class InPatient
    {
        private CacheDB db;
        public InPatient() {
            db = new CacheDB();
        }
        /// <summary>
        /// 全院各科室在院患者人数,返回两列(科室，人数)
        /// </summary>
        public  string GetInPatientCount() {
           
            DataSet ds = db.Query("CALL web.LocIpAdmRS()");
            StringBuilder sb = new StringBuilder();
            if (ds.Tables.Count > 0) {
                var sum = 0;
                sb.AppendLine(string.Format("截止到{0}全院各科室住院人数",DateTime.Now));
                foreach (DataRow dr in ds.Tables[0].Rows) {
                    sb.AppendLine(string.Format("{0}:{1}", dr[0], dr[1]));
                    sum += (int)(dr[1]);
                }
                sb.AppendLine(string.Format("合计{0}",sum));
                return sb.ToString();
            }
            return null;
        }
    }
}
