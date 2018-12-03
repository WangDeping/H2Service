using H2Service.WeChatWork;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace App.Helper
{
    public class WxJsSignatureHelper
    {
        private readonly IWxAppService _wxAppService;
        public WxJsSignatureHelper(IWxAppService wxAppService)
        {
            _wxAppService = wxAppService;

        }

        public  static long CreatenTimestamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds);
        }

        private static string[] strs = new string[]
                                 {
                                  "a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z",
                                  "A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z"
                                 };
        /// </summary>
        /// <returns></returns>
        public static string CreatenNonce()
        {
            Random r = new Random();
            var sb = new StringBuilder();
            var length = strs.Length;
            for (int i = 0; i < 15; i++)
            {
                sb.Append(strs[r.Next(length - 1)]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 生成JSApi签名
        /// </summary>
        /// <param name="jsTicket"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static Hashtable GetParameters( string jsTicket, string url)
        {
            string appId = WebConfigurationManager.AppSettings["corpid"];
            string timestamp = CreatenTimestamp().ToString();
            string nonceStr = CreatenNonce();
           
            // 这里参数的顺序要按照 key 值 ASCII 码升序排序  
            string rawstring = "jsapi_ticket=" + jsTicket + "&noncestr=" + nonceStr + "&timestamp=" + timestamp + "&url=" + url + "";
            
            string signature = GetSignature(rawstring);
            Hashtable signPackage = new Hashtable();
            signPackage.Add("appid", appId);
            signPackage.Add("noncestr", nonceStr);
            signPackage.Add("timestamp", timestamp);
            signPackage.Add("url", url);
            signPackage.Add("signature", signature);
            signPackage.Add("jsapi_ticket", jsTicket);
            signPackage.Add("rawstring", rawstring);

            return signPackage;
        }
        private static string GetSignature(string rawstring)
        {
            SHA1 sha1 = new SHA1CryptoServiceProvider();
            byte[] bytes_sha1_in = System.Text.UTF8Encoding.Default.GetBytes(rawstring);
            byte[] bytes_sha1_out = sha1.ComputeHash(bytes_sha1_in);
            string signature = BitConverter.ToString(bytes_sha1_out);
            signature = signature.Replace("-", "").ToLower();
            return signature;           
        }
    }
}