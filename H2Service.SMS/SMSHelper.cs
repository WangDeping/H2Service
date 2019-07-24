using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace H2Service.SMS
{
 internal static   class SMSHelper
    {
        public static string postData(string postUrl, Dictionary<string, string> paramData)
        {
            string reuslt = "";
            try
            {
                StringBuilder builder = new StringBuilder();
                int i = 0;
                foreach (var item in paramData)
                {
                    if (i > 0)
                        builder.Append("&");
                    builder.AppendFormat("{0}={1}", item.Key, item.Value);
                    i++;
                }                
                byte[] byteArray = Encoding.UTF8.GetBytes(builder.ToString());
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                webReq.ContentLength = byteArray.Length;
                Stream newStream = webReq.GetRequestStream();
                newStream.Write(byteArray, 0, byteArray.Length);
                newStream.Close();
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                reuslt = sr.ReadToEnd();
                sr.Close();
                response.Close();
            }
            catch (Exception e)
            {
                reuslt = "-1";
                //错误日志
            }
            return reuslt;
        }
    }
}
