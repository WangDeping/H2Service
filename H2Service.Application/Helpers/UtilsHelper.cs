using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace H2Service.Extensions
{
 public  static  class UtilsHelper
    {
        public static string ModelToUriParam(this object obj, string url = "")
        {
            PropertyInfo[] propertis = obj.GetType().GetProperties();
            StringBuilder sb = new StringBuilder();
            sb.Append(url);
            sb.Append("?");
            foreach (var p in propertis)
            {
                var v = p.GetValue(obj, null);
                if (v == null)
                    continue;
                sb.Append(p.Name);
                sb.Append("=");
                sb.Append(HttpUtility.UrlEncode(v.ToString()));
                sb.Append("&");
            }
            sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string SerializeJson<T>(this T obj)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(obj);
        }
        public static T GetObjectFromJson<T>(this object obj2, string json)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            T obj = (T)ser.ReadObject(ms);
            return obj;

        }


    }
}
