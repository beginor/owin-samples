using System;
using Microsoft.Owin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Owin03_Middleware {

    using AppFunc = Func<IDictionary<string, object>, Task>;

    public class InstanceMiddleware {

        private AppFunc next;

        public void Initialize(AppFunc next) {
            this.next = next;
        }

        public async Task Invoke(IDictionary<string, object> env) {
            Console.WriteLine("InstanceMiddleware Start.");
            await next(env);
            Console.WriteLine("InstanceMiddleware End.");
        }
    }
}

