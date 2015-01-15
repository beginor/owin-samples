using System;
using Microsoft.Owin.Builder;
using Nowin;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;

namespace Owin02_WebApi_Nowin {

    class MainClass {

        public static void Main(string[] args) {
            var appBuilder = new AppBuilder();
            Nowin.OwinServerFactory.Initialize(appBuilder.Properties);

            var startup = new Owin02_WebApi.Startup();
            startup.Configuration(appBuilder);

            var builder = new ServerBuilder();
            var ip = "127.0.0.1";
            var port = 8888;
            builder.SetAddress(System.Net.IPAddress.Parse(ip)).SetPort(port)
                .SetOwinApp(appBuilder.Build())
                .SetOwinCapabilities((IDictionary<string, object>)appBuilder.Properties[OwinKeys.ServerCapabilitiesKey]);

            using (var server = builder.Build()) {

                Task.Run(() => server.Start());

                var baseAddress = "http://" + ip + ":" + port + "/";
                Console.WriteLine("Nowin server listening " + baseAddress);

                var client = new HttpClient {
                    BaseAddress = new Uri(baseAddress, UriKind.Absolute)
                }; 

                var requestTask = client.GetAsync("api/values");
                requestTask.Wait();
                var response = requestTask.Result; 
                Console.WriteLine(response);

                var readTask = response.Content.ReadAsStringAsync();
                readTask.Wait();
                Console.WriteLine(readTask.Result);

                Console.ReadLine();
            }
        }
    }
}
