using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using EMP.DataAccess.Context;

namespace EMP.Api
{
    public class Program
    {
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .AddJsonFile($"appsettings.Development.json", true)
            .AddEnvironmentVariables()
            .Build();

        public static Logger Logger { get; } = new LoggerConfiguration()
            .ReadFrom.Configuration(Configuration)
            .Enrich.FromLogContext()
            .CreateLogger();

        public static int Main(string[] args)
        {
            Log.Logger = Logger;

            try {
                Log.Information("Starting up");
                IHost host = CreateHostBuilder(args).Build();
                // SeedEmployeeDatabase(host);
                host.Run();
                return 0;
            } catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
                return 1;
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog((hostingContext, loggerConfig) =>
                    loggerConfig.ReadFrom.Configuration(hostingContext.Configuration)
                )
                .ConfigureLogging(logging => {
                    logging.ClearProviders();
                })
                // .ConfigureAppConfiguration(SetupConfiguration)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });

        // private static void SetupConfiguration(HostBuilderContext ctx, IConfigurationBuilder builder)
        // {
        //     // builder.Sources.Clear();
        //     builder
        //         .AddJsonFile("Config/config.json", false, true)
        //         .AddEnvironmentVariables();
        // }

        // private static void SeedEmployeeDatabase(IHost host)
        // {
        //     // Trying to keep seeder withing a scope
        //     var scopeFactory = host.Services.GetService<IServiceScopeFactory>();
        //     using (var scope = scopeFactory.CreateScope()) {
        //         // var seeder = host.Services.GetService<EmployeesDataSeeder>();
        //         var seeder = scope.ServiceProvider.GetService<EmployeesDataSeeder>();
        //         seeder.Seed();
        //     }
        // }
    }
}
