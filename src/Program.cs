using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore;

namespace restaurant_dashboard_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        // private static string GetContentRoot() {
        //     var root = Directory.GetCurrentDirectory();
        //     var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        //     if(env == "production") {
        //         root += "/dist";
        //     }

        //     return root;
        // }
    }
}
