using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.Web.Helpers;
using System.Security.Claims;

[assembly: OwinStartup(typeof(Practica.Startup))]

namespace Practica
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpListener listener =
            //      (HttpListener)app.Properties["System.Net.HttpListener"];
            //listener.AuthenticationSchemes =
            //    AuthenticationSchemes.Anonymous;
            //app.Run(context =>
            //{
            //    context.Response.ContentType = "text/plain";
            //    return context.Response.WriteAsync("Hello World!");
            //});
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Usuarios/IniciarSesion")
            });
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;

        }
    }
}
