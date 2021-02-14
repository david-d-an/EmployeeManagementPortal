using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.DataAccess.Context;
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
            IHost host = CreateHostBuilder(args).Build();
            // SeedEmployeeDatabase(host);
            host.Run();
        }

        private static void SeedEmployeeDatabase(IHost host)
        {
            // Trying to keep seeder withing a scope
            var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope()) {
                // var seeder = host.Services.GetService<EmployeesDataSeeder>();
                var seeder = scope.ServiceProvider.GetService<EmployeesDataSeeder>();
                seeder.Seed();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                // .ConfigureServices((context, services) => {
                //     services.Configure<KestrelServerOptions>(
                //         context.Configuration.GetSection("Kestrel"));
                // })
                .ConfigureAppConfiguration(SetupConfiguration)
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

        private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        {
            // builder.Sources.Clear();
            builder
                .AddJsonFile("config.json", false, true)
                .AddEnvironmentVariables();
        }
    }
}
