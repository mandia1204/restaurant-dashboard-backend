using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore;

namespace restaurant_dashboard_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging((_, builder) => {
                    builder.ClearProviders();
                    builder.AddConsole();
                    builder.AddDebug();
                })
                .UseStartup<Startup>()
                .UseUrls("http://*:5000")
                .UseContentRoot(GetContentRoot());

        private static string GetContentRoot() {
            var root = Directory.GetCurrentDirectory();
            Console.WriteLine("root:{0}", root);
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            Console.WriteLine("env:{0}", env);
            
            // if(env == "production") {
            //     root += "\\backend";
            // }

            return root;
        }
    }
}
