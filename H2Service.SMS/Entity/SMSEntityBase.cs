using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace H2Service.SMS.Entity
{
    internal class SMSEntityBase
    {
        internal SMSEntityBase()
        {
            this.appId = WebConfigurationManager.AppSettings["smsAppId"];
            var secretkey = WebConfigurationManager.AppSettings["smsSecretkey"];
           this.timestamp = DateTime.Now.ToString("yyyyMMddHHmmss");
           var encrt = this.appId+secretkey+ this.timestamp;         
           var md5 = MD5.Create();
           var result = md5.ComputeHash(Encoding.ASCII.GetBytes(encrt));        
            this.sign = BitConverter.ToString(result).Replace("-", ""); 
        }

        internal string appId { get; }

        internal string timestamp { get; }

        internal string sign { get;}

        
    }
}
