using System;
using System.Linq;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using EMP.Sts.Data;
using EMP.Sts.Models;
using EMP.Sts.Quickstart.Account;
using Microsoft.Extensions.Logging;
using EMP.Sts.Security;

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
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var identityConfig = Config.GetIdentityResources();
            var apiConfig = Config.GetApiResources(Configuration, _logger);
            var clientConfig = Config.GetClients(Configuration, _logger);
            var publicOrigin = Config.GetPublicOrigin(Configuration, _logger);

            services.AddDbContext<ApplicationDbContext>(option =>
                option.UseMySQL(Configuration.GetConnectionString("DefaultConnection")));

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

            var builder = services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.Authentication.CookieLifetime = TimeSpan.FromMinutes(15);
                    // PublicOrigin is reqruied to stand behind Reverse Proxy
                    options.PublicOrigin = publicOrigin;
                })
                .AddInMemoryIdentityResources(identityConfig)
                .AddInMemoryApiResources(apiConfig)
                .AddInMemoryClients(clientConfig)
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>();

            var rsa = new RsaKeyService(Environment, TimeSpan.FromDays(30));
            services.AddSingleton<RsaKeyService>(provider => rsa);

            if (Environment.IsDevelopment()) {
                builder.AddDeveloperSigningCredential();
            }
            else {
                // To Do: Figure out why Regualar signing doesn't work
                builder.AddDeveloperSigningCredential();
                // builder.AddSigningCredential(rsa.GetKey());
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

            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}
