using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Owin04_OAuthClient {

    public class Global : HttpApplication {

        protected void Application_Start() {
            RegisterRoutes(RouteTable.Routes);
        }

        public static void RegisterRoutes(RouteCollection routes) {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}