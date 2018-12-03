using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace H2Service
{
    public static class Helper
    {
        //获取目标url的utf8编码返回值
        public static string GetURLResultByUTF8(string url)
        {

            WebClient wc = new WebClient();
            Byte[] pageData = wc.DownloadData(url);
            string result = Encoding.GetEncoding("utf-8").GetString(pageData);
            return result;
        }
        public static string DownLoadWxTempFile(string url,string path)
        {
            HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            string relPath = response.Headers["Content-disposition"].Split('"')[1];//文件名
            string filePath=path+relPath;//物理路径
            Stream stream= response.GetResponseStream();
            //var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8"));
            FileStream fs = new FileStream(filePath, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size = stream.Read(bArr, 0, bArr.Length);
            while (size > 0)
            {
                 fs.Write(bArr, 0, size);
                size = stream.Read(bArr, 0, bArr.Length);
             }
             fs.Close();
             stream.Close();
            return relPath;
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