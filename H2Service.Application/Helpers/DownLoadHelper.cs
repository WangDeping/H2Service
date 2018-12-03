using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace H2Service.Helpers
{
 public   class DownLoadHelper
    {
        private readonly string avatarPath;
        private ILogger logger;
        public DownLoadHelper()
        {
            logger = NullLogger.Instance;
            if (HttpContext.Current != null)
            {
                avatarPath = HttpContext.Current.Server.MapPath(@"~/Content/avatars/");               
            }
            else
            {
                avatarPath = @AppDomain.CurrentDomain.BaseDirectory + "Content/avatars/";               
                
            }

        }
        
        public void DownloadAvatar(string url, string saveName)
        {
            try
            {
                var smallUrl = "";
                if(url.Substring(url.Length-2)==@"/0")               
                    smallUrl = url.Substring(0,url.Length-2) + @"/100";
                else
                    smallUrl = url + "100";
                WebClient client = new WebClient();
                var mybyte = client.DownloadData(smallUrl);
                MemoryStream ms = new MemoryStream(mybyte);
                var img = System.Drawing.Image.FromStream(ms);
                img.Save(avatarPath + saveName + ".jpg", ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

        }
    }
}
