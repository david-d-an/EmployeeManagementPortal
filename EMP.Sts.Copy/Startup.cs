using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using IdentityServer4.Services;
using Serilog;
using EMP.Sts.Data;
using EMP.Sts.Models;
using EMP.Sts.Quickstart.Account;
using EMP.Sts.Security;
using EMP.Common.Security;
using IdentityServer4.Models;

namespace EMP.Sts
{
    public class Startup
    {
        private IWebHostEnvironment _env;
        public IConfiguration Configuration { get; }
        private readonly string EmpWebOrigins = "EMP.Web";
        private IEnumerable<IdentityResource> _identityConfig;
        private IEnumerable<ApiResource> _apiConfig;
        private IEnumerable<Client> _clientConfig;
        private string _publicOrigin;
        private int _cookieExpiration;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _identityConfig = Config.GetIdentityResources();
            _apiConfig = Config.GetApiResources(Configuration);
            _clientConfig = Config.GetClients(Configuration);
            _publicOrigin = Config.GetPublicOrigin(Configuration);
            _cookieExpiration = Config.GetCookieExpirationByMinute(Configuration);

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
                        _clientConfig.First().AllowedCorsOrigins.ToArray()
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
                options.PublicOrigin = _publicOrigin;
            })
            .AddInMemoryIdentityResources(_identityConfig)
            .AddInMemoryApiResources(_apiConfig)
            .AddInMemoryClients(_clientConfig)
            .AddAspNetIdentity<ApplicationUser>()
            .AddProfileService<CustomProfileService>();

            services.ConfigureApplicationCookie(options => {
                // To prevent Refresh Access Token overriding cookie refreshe, subtract 1 minute
                options.ExpireTimeSpan = TimeSpan.FromMinutes(_cookieExpiration - 1);
                options.SlidingExpiration = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
            });

            if (_env.IsDevelopment()) {
                // builder.AddDeveloperSigningCredential();
                var rsa = new RsaKeyService(_env, TimeSpan.FromDays(30), Configuration);
                services.AddSingleton<RsaKeyService>(provider => rsa);
                builder.AddSigningCredential(rsa.GetKey());
            }
            else {
                var rsa = new RsaKeyService(_env, TimeSpan.FromDays(30), Configuration);
                services.AddSingleton<RsaKeyService>(provider => rsa);
                builder.AddSigningCredential(rsa.GetKey());

                // services.AddIdentityServer(...).AddSigningCredential(new X509Certificate2(bytes, "password")
                // builder.AddSigningCredential(new X509Certificate2(bytes, "password");
            }
        }

        public void Configure(IApplicationBuilder app, ILogger<Startup> logger)
        {
            // logger.LogInformation(string.Format("{0}: {1}", "Test Message for", "INFORMATION"));
            // logger.LogDebug(string.Format("{0}: {1}", "Test Message for", "DEBUG"));
            // logger.LogError(string.Format("{0}: {1}", "Test Message for", "ERROR"));
            // logger.LogWarning(string.Format("{0}: {1}", "Test Message for", "WARNING"));
            // logger.LogCritical(string.Format("{0}: {1}", "Test Message for", "CRITICAL"));
            
            foreach (IdentityResource ic in _identityConfig) {
                logger.LogInformation(string.Format("{0}: {1}", "Identity Resource Name", ic.Name));
                logger.LogInformation(string.Format("{0}: {1}", "Identity Resource DisplayName", ic.DisplayName));
            }
            foreach (ApiResource ac in _apiConfig) {
                logger.LogInformation(string.Format("{0}: {1}", "API Name", ac.Name));
                logger.LogInformation(string.Format("{0}: {1}", "API DisplayName", ac.DisplayName));
            }

            foreach (Client c in _clientConfig) {
                logger.LogInformation(string.Format("{0}: {1}", "ClientId", c.ClientId));
                logger.LogInformation(string.Format("{0}: {1}", "ClientName", c.ClientName));
                logger.LogInformation(string.Format("{0}: {1}", "RequirePkce", c.RequirePkce));

                foreach(var s in c.RedirectUris) {
                    logger.LogInformation(string.Format("{0}: {1}", "RedirectUris", s));
                }
                foreach(var s in c.PostLogoutRedirectUris) {
                    logger.LogInformation(string.Format("{0}: {1}", "PostLogoutRedirectUris", s));                
                }
                foreach(var s in c.AllowedCorsOrigins) {
                    logger.LogInformation(string.Format("{0}: {1}", "AllowedCorsOrigins", s));                    
                }
            }
            
            if (_env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            } else {
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
