using App.App_Start;
using Abp.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace App.App_Start
{
    public class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void Configuration(IAppBuilder app)
        {
            app.UseAbp();
          

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Auth/Index")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.MapSignalR();
        }
    }
}