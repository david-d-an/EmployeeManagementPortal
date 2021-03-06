using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog.Sinks.SystemConsole.Themes;

namespace EMP.Sts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override(
                    "Microsoft.AspNetCore.Authentication", 
                    LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                // .WriteTo.Console(
                //     outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", 
                //     theme: AnsiConsoleTheme.Literate)
                .WriteTo.File(
                    new RenderedCompactJsonFormatter(), 
                    "logs/log-.ndjson", 
                    rollingInterval: RollingInterval.Day)
                .CreateLogger();

            try {
                Log.Information("Starting up");
                BuildWebHost(args).Run();
            } catch (Exception ex) {
                Log.Fatal(ex, "Application start-up failed");
            } finally {
                Log.CloseAndFlush();
            }
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                // .UseUrls("http://localhost:14242/")
                .ConfigureLogging(builder => {
                    builder.ClearProviders();
                    // builder.AddSerilog();
                })
                .UseStartup<Startup>()
                .Build();
    }
}
