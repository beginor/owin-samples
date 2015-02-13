using System;
using System.Linq;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Owin05_Cookie_Auth.Startup))]

namespace Owin05_Cookie_Auth {

    public class Startup {

        public void Configuration(IAppBuilder app) {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/api/account"),
                Provider = new CookieAuthenticationProvider {
                    OnApplyRedirect = OnApplyRedirect
                }
            });

            var config = new HttpConfiguration();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }

        private void OnApplyRedirect(CookieApplyRedirectContext context) {
            var isAjaxRequest = false;
            string[] requestWith;
            if (context.Request.Headers.TryGetValue("X-Requested-With", out requestWith)) {
                if (requestWith != null && requestWith.Any(s => s == "XMLHttpRequest")) {
                    isAjaxRequest = true;
                }
            }

            if (!isAjaxRequest) {
                context.Response.Redirect(context.RedirectUri);
            }
        }
    }
}
