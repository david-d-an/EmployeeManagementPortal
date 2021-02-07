using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
//
namespace EMP.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .ConfigureServices((context, services) => {
                //     services.Configure<KestrelServerOptions>(
                //         context.Configuration.GetSection("Kestrel"));
                // })
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddDebug();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseKestrel(options => {
                    //     options.Limits.MaxRequestBodySize = null;
                    // });
                    // webBuilder.ConfigureKestrel(options => {
                    //     // Set properties and call methods on options
                    // });
                });
    }
}
