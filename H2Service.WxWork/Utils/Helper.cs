using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace H2Service.WxWork.Utils
{
 public   class Helper
    {
        //获取目标url的utf8编码返回值
        public static string GetURLResultByUTF8(string url)
        {

            WebClient wc = new WebClient();
            Byte[] pageData = wc.DownloadData(url);
            string result = Encoding.GetEncoding("utf-8").GetString(pageData);
            return result;
        }       
        public static string JsonSerialize(object data)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(data);
        }
        public static T JsonDeserialize<T>(string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            T obj = (T)ser.ReadObject(ms);
            return obj;

        }
    }
}
