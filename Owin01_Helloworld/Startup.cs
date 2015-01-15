using Owin;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace Owin01_Helloworld {

    public class Startup {

        public void Configuration(IAppBuilder appBuilder) {
            appBuilder.Run(HandleRequest);
        }

        static Task HandleRequest(IOwinContext context) {
            context.Response.ContentType = "text/plain";
            return context.Response.WriteAsync("Hello, world!");
        }
    }
}

