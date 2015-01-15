using System;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin03_Middleware {

    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class LogMiddleware  {

        private readonly AppFunc next;

        public LogMiddleware(AppFunc next) {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env) {
            Console.WriteLine("LogMiddleware Start.");
            await next(env);
            Console.WriteLine("LogMiddleware End.");
        }
    }
}

