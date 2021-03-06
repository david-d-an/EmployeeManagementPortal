using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog.Core;

namespace EMP.Sts
{
    public class Program
    {
        private static bool IsDevelopment =>
            Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

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
                .ConfigureLogging(builder => {
                    builder.ClearProviders();
                    // builder.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
