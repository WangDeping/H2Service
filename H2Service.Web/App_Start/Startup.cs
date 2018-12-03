using Abp.Owin;
using H2Service.Hangfire.Jobs.DailyServerRoomPatrol;
using H2Service.Hangfire.Jobs.DailyServerRoomPatrol.Dto;
using H2Service.Hangfire.Jobs.DailyUserSsynchronous;
using H2Service.Hangfire.Jobs.DailyUserSsynchronous.Dto;
using H2Service.Hangfire.Jobs.WeeklyUserDetailUpdate;
using H2Service.Hangfire.Jobs.WeeklyUserDetailUpdate.Dto;
using H2Service.Web;
using Hangfire;
using Hangfire.Dashboard;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace H2Service.Web
{
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {
        /// <summary>
        ///     Determines whether a user may access the hangfire dashboard.
        /// </summary>
        /// <param name="aContext">Context we are accessing the dashboard in.</param>
        /// <returns>Returns TRUE should the user be allowed to access the dashboard.</returns>
        public bool Authorize(DashboardContext aContext)
        {
            // In case you need an OWIN context, use the next line, `OwinContext` class
            // is the part of the `Microsoft.Owin` package.
            OwinContext owinContext = new OwinContext(aContext.GetOwinEnvironment());
            //throw new Exception(HttpContext.Current.User.Identity.GetUserName());
           // HttpContext.Current.Response.Redirect(owinContext.Authentication.User.Claims.FirstOrDefault(C => C.Type == "Permissions").Value);
               
            return owinContext.Authentication.User.Identity.IsAuthenticated;
           


        }
        
    }
   
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage("Default");
            

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Index")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
          
            app.MapSignalR();
            BackgroundJobServerOptions serverOptions = new BackgroundJobServerOptions
            {
            };
            app.UseHangfireServer(serverOptions);
            //app.UseHangfireDashboard();
            var options = new DashboardOptions { Authorization = new[] { new HangfireAuthorizationFilter() } };          
            app.UseHangfireDashboard("/hangfire",options);
            app.UseAbp();
            //任务
            RecurringJob.AddOrUpdate<DailyServerRoomPatrolJob>("机房日常巡视提醒", x => x.ExecuteJob(new DailyServerRoomPatrolJobArgs()), "00 10,16 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<DailyUserSynchronousJob>("同步企业微信用户任务", x => x.ExecuteJob(new DailyUserSynchronousJobArgs()), Cron.Daily);
            RecurringJob.AddOrUpdate<WeeklyUserDetailUpdateJob>("用户性别/头像更新", X => X.ExecuteJob(new WeeklyUserDetailUpdateJobArgs()), Cron.Weekly);
        }
    }
}
