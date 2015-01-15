using System;
using Microsoft.Owin;
using System.Threading.Tasks;

namespace Owin03_Middleware {

    public class LogOwinMiddleware : OwinMiddleware {

        public LogOwinMiddleware(OwinMiddleware next) : base(next) {
        }

        public async override Task Invoke(IOwinContext context) {
            Console.WriteLine("LogOwinMiddleware Start.");
            await Next.Invoke(context);
            Console.WriteLine("LogOwinMiddleware End.");
        }

    }
}
