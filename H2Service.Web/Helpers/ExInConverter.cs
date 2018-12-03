using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace H2Service.Web.Helpers
{
    public static class ExInConverter
    {
        public static string Ex2In(string url)
        {
            var extranet = WebConfigurationManager.AppSettings["extranet"];
            var intranet= WebConfigurationManager.AppSettings["intranet"];
            if (url.Contains(extranet))
                url.Replace(extranet, intranet);
            return url;
        }


        public static string In2Ex(string url)
        {
            var extranet = WebConfigurationManager.AppSettings["extranet"];
            var intranet = WebConfigurationManager.AppSettings["intranet"];
            if (url.Contains(intranet))
                url.Replace(intranet, extranet);
            return url;


        }
    }
}