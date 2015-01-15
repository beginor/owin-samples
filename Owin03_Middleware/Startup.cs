using System;
using Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin03_Middleware {

    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class Startup {

        public void Configuration(IAppBuilder app) {

            app.Use(new Func<AppFunc, AppFunc>(next => (async env => {
                Console.WriteLine("Middleware with AppFunc begin.");
                await next.Invoke(env);
                Console.WriteLine("Middleware with AppFunc end.");
            })));

            app.Use(async (context, next) => {
                Console.WriteLine("Middleware with Lambda begin.");
                await next();
                Console.WriteLine("Middleware with Lambda end.");
            });

            app.Use<LogMiddleware>();

            var instance = new InstanceMiddleware();
            app.Use(instance);

            app.Use<LogOwinMiddleware>();

            app.Run(async context => {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("Hello, world!");
            });

        }
    }
}

