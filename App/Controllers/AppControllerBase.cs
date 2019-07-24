using Abp.Authorization;
using Abp.Dependency;
using Abp.Runtime.Validation;
using Abp.Web.Mvc.Controllers;
using App.Helper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using H2Service.WxWork;
using System;
using System.Collections;

using System.Web.Mvc;

namespace App.Controllers
{
    public class AppControllerBase : AbpController
    {
      
        public AppControllerBase()
        {
           
        }

        protected void GetWxJSApiSignature(string ticket)
        {
             string url = Request.Url.ToString();           
            int endIndex = url.IndexOf('#');
            if (endIndex > 0)
                url = url.Substring(0, endIndex);           
            Hashtable table = WxJsSignatureHelper.GetParameters(ticket, url);           
            ViewBag.appid = table["appid"];
            ViewBag.noncestr = table["noncestr"];
            ViewBag.timestamp = table["timestamp"];
            ViewBag.signature = table["signature"];            
        }
        //protected override void OnException(ExceptionContext context)
        //{
        //    if (!context.ExceptionHandled)
        //    {              
        //        context.ExceptionHandled = true;
        //    }
        //    base.OnException(context);
           
        //}
        public ActionResult Error()
        {
            
            return View();
        }
    }
}