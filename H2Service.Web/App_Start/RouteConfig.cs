﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace H2Service.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //ASP.NET Web API Route Config
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
                );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                name:"工资上传",
                url: "{controller}/{action}/{period}/{typeId}",
                defaults:new { controller="Salary",action= "SalaryUpload",typeId=UrlParameter.Optional}
                );
            routes.MapRoute(
                name: "二维码",
                url: "{controller}/{action}/{content}",
                defaults: new { controller = "QrCode", action = " GetQrCode", content = UrlParameter.Optional }
                );
        }
    }
}
