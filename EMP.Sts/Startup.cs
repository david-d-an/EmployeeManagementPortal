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

namespace EMP.Sts
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }
        private readonly string EmpWebOrigins = "EMP.Web";

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
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
                        "http://localhost:5000",
                        "https://localhost:5001",
                        "http://ipv4.fiddler:5000",
                        "https://ipv4.fiddler:5001"
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
                })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>()
                .AddProfileService<CustomProfileService>();


            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
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
