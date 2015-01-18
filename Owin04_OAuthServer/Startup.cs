using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Owin04_OAuthServer.Startup))]

namespace Owin04_OAuthServer {

    public partial class Startup {

        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }

    }
}

