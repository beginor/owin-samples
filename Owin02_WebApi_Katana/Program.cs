using System;
using Microsoft.Owin.Hosting;
using System.Net.Http;

namespace Owin02_WebApi_Katana {

    class MainClass {

        public static void Main(string[] args) {

            var baseAddress = "http://localhost:9000/";

            var startOpts = new StartOptions(baseAddress) {
                ServerFactory = "Microsoft.Owin.Host.HttpListener" // katana http listener
            };

            using (WebApp.Start<Owin02_WebApi.Startup>(startOpts)) {
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
