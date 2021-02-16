using System;
using System.Linq;
using EMP.Data.Repos;
using EMP.Common.Security;
using EMP.DataAccess.Repos;
using EMP.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EMP.Data.Models;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using EMP.Api.Config;
// using System.Collections.Generic;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Net.Http.Headers;

namespace EMP.Api
{
    public class Startup
    {
        // private readonly ILogger<Startup> _logger;
        private readonly string EmpWebOrigins = "EMP.Web";

        // To Do: Figure out why ILogger causes runtine error for dependency
        public Startup(IConfiguration configuration)//, ILogger<Startup> logger)
        {
            // _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var securitySettings = ApiConfig.GetSecuritySettings(Configuration);//, _logger);

            services.AddCors(options =>
            {
                options.AddPolicy(name: EmpWebOrigins, builder => {
                    builder
                    .AllowAnyHeader()
                    // .WithHeaders(HeaderNames.AccessControlAllowHeaders, "Content-Type")
                    // .AllowAnyOrigin()
                    .WithOrigins(
                        securitySettings.AllowedCorsOrigins.ToArray()
                    )
                    .AllowAnyMethod();
                    // .WithMethods("GET", "PUT", "POST", "DELETE");
                });
            });

            services
            .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            .AddIdentityServerAuthentication(options => {
                options.Authority = securitySettings.StsAuthority;
                options.ApiName = securitySettings.ApiName;
                options.RequireHttpsMetadata = false;
            });

            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddControllers();

            // var a = AesCryptoUtil.GetStringSha256Hash("Soil9303");
            // Before Hash: Soil9303
            // After Hash: 6D2450AD484CF4C9F99007D0C4D0E2D694F110BB580D74062E3A3A79F33E432C

            // Make MySql Connection Service
            var encConnStrMySql = Configuration.GetConnectionString("MySqlConnection(Azure)");
            var connStrMySql = AesCryptoUtil.Decrypt(encConnStrMySql);

            services.AddDbContext<EmployeesContext>(builder =>                   
                builder.UseMySQL(connStrMySql)
            );
            EnsureDatabaseExists<EmployeesContext>(connStrMySql);

            services.AddTransient<EmployeesDataSeeder>();

            services.AddScoped<IRepository<DeptManager>, DeptManagerRepository>();
            services.AddScoped<IRepository<VwDeptManagerDetail>, DeptManagerDetailRepository>();
            services.AddScoped<IRepository<Departments>, DepartmentsRepository>();
            services.AddScoped<IRepository<VwDeptEmpCurrent>, DeptEmpRepository>();
            services.AddScoped<IRepository<VwDeptManagerCurrent>, DeptManagerCurrentRepository>();
            services.AddScoped<IRepository<VwEmpDetails>, EmployeeDetailRepository>();
            services.AddScoped<IRepository<VwEmpDetailsShort>, EmployeeDetailShortRepository>();
            services.AddScoped<IRepository<Employees>, EmployeeRepository>();
            services.AddScoped<IRepository<VwTitlesCurrent>, TitleRepository>();
            services.AddScoped<IRepository<DistinctTitles>, DistinctTitleRepository>();
            services.AddScoped<IRepository<DistinctGenders>, DistinctGenderRepository>();
            services.AddScoped<IRepository<VwSalariesCurrent>, SalaryRepository>();
        }

        private static void EnsureDatabaseExists<T>(string connectionString) 
        where T : DbContext, new()
        {
            var builder = new DbContextOptionsBuilder<T>();
            if (typeof(T) == typeof(EmployeesContext)) {
                builder.UseMySQL(connectionString);
            }
            // else if (typeof(T) == typeof(SQLiteContext)) {
            // builder.UseSqlite(connectionString);
            // }
            // else if (typeof(T) == typeof(SqlServerContext)) {
            // builder.UseSqlServer(connectionString);
            // }

            using var context = (T)Activator.CreateInstance(typeof(T), 
                new object[] { builder.Options });
            context.Database.EnsureCreated();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(EmpWebOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
