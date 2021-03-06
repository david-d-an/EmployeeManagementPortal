using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using EMP.Sts.Data;
using EMP.Sts.Models;
using EMP.Sts.Quickstart.Account;
using EMP.Sts.Security;
using EMP.Common.Security;

namespace EMP.Sts
{
    public class Startup
    {
        private readonly ILogger<Startup> _logger;
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        private readonly string EmpWebOrigins = "EMP.Web";

        public Startup(ILogger<Startup> logger, IConfiguration configuration, IHostingEnvironment environment)
        {
            _logger = logger;
            Configuration = configuration;
            Environment = environment;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var identityConfig = Config.GetIdentityResources();
            var apiConfig = Config.GetApiResources(Configuration, _logger);
            var clientConfig = Config.GetClients(Configuration, _logger);
            var publicOrigin = Config.GetPublicOrigin(Configuration, _logger);

            var encConnStrMySql = Configuration.GetConnectionString("MySqlConnection(Azure)");
            var connStrMySql = AesCryptoUtil.Decrypt(encConnStrMySql);

            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseMySQL(connStrMySql));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                options.AddPolicy(EmpWebOrigins, corsBuilder =>
                {
                    corsBuilder
                    .AllowAnyHeader()
                    // .WithHeaders(HeaderNames.AccessControlAllowHeaders, "Content-Type")
                    // .AllowAnyOrigin()
                    .WithOrigins(
                        clientConfig.First().AllowedCorsOrigins.ToArray()
                    )
                    .AllowAnyMethod()
                    // .WithMethods("GET", "PUT", "POST", "DELETE")
                    .AllowCredentials();
                });
            });

            services.AddMvc();
            services.AddTransient<IProfileService, CustomProfileService>();

            var builder = services.AddIdentityServer(options => {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.Authentication.CheckSessionCookieName = "IDS4_EMP.Sts";
                options.PublicOrigin = publicOrigin;
            })
            // .AddInMemoryIdentityResources(Config1.GetIdentityResources())
            // .AddInMemoryApiResources(Config1.GetApiResources())
            // .AddInMemoryClients(Config1.GetClients())
            .AddInMemoryIdentityResources(identityConfig)
            .AddInMemoryApiResources(apiConfig)
            .AddInMemoryClients(clientConfig)
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<CustomProfileService>();

            services.ConfigureApplicationCookie(options => {
                // To prevent Refresh Access Token overriding cookie refreshe, subtract 1 minute
                options.ExpireTimeSpan = TimeSpan.FromMinutes(
                    Config.GetCookieExpirationByMinute(Configuration) - 1
                );
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest;
            });

            if (Environment.IsDevelopment()) {
                // builder.AddDeveloperSigningCredential();
                var rsa = new RsaKeyService(Environment, TimeSpan.FromDays(30), Configuration);
                services.AddSingleton<RsaKeyService>(provider => rsa);
                builder.AddSigningCredential(rsa.GetKey());
            }
            else {
                var rsa = new RsaKeyService(Environment, TimeSpan.FromDays(30), Configuration);
                services.AddSingleton<RsaKeyService>(provider => rsa);
                builder.AddSigningCredential(rsa.GetKey());

                // services.AddIdentityServer(...).AddSigningCredential(new X509Certificate2(bytes, "password")
                // builder.AddSigningCredential(new X509Certificate2(bytes, "password");
            }
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseCors(EmpWebOrigins);

            // var fordwardedHeaderOptions = new ForwardedHeadersOptions {
            //     ForwardedHeaders = 
            //         ForwardedHeaders.XForwardedFor | 
            //         ForwardedHeaders.XForwardedProto
            // };
            // fordwardedHeaderOptions.KnownNetworks.Clear();
            // fordwardedHeaderOptions.KnownProxies.Clear();
            // app.UseForwardedHeaders(fordwardedHeaderOptions);

            app.UseIdentityServer();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
