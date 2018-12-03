using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data.Odbc;
using System.Data;
namespace H2Service.DHCQuery
{
 public   class CacheDB
    {
        private string constr = "DSN=DHCAPP;uid=_system;pwd=sys;";
        
        /// <summary>
        /// 查询并返回数据
        /// </summary>
        /// <param name="cmdtext"></param>
        /// <returns></returns>
        public DataSet Query(string cmdtext) {

            OdbcConnection connection = new OdbcConnection(constr);
            connection.Open();

            OdbcDataAdapter adapter = new OdbcDataAdapter(cmdtext,connection);
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            connection.Close();
            return ds;

            
        }
    }
}