using System;
using Microsoft.Owin.Hosting;

namespace Owin01_Helloworld {

    class MainClass {

        public static void Main(string[] args) {
            var url = "http://localhost:8080/";
            var startOpts = new StartOptions(url) {

            };
            using (WebApp.Start<Startup>(startOpts)) {
                Console.WriteLine("Server run at " + url + " , press Enter to exit.");
                Console.ReadLine();
            }
        }
    }
}
