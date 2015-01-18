using System.Web;
using System.Web.Routing;

namespace Owin04_OAuthServer {

    public class Global : HttpApplication {

        protected void Application_Start() {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

    }
}