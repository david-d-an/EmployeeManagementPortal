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
using EMP.Data.Models.Employees;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using EMP.Api.Config;
using EMP.Data.Models.Sts;

namespace EMP.Api
{
    public class Startup
    {
        private SecuritySettings securitySettings;
        private readonly string EmpWebOrigins = "EMP.Web";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            this.securitySettings = ApiConfig.GetSecuritySettings(Configuration);

            services.AddCors(options => {
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

            services.AddResponseCaching();
            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            });

            services.AddControllers();

            // Make MySql Connection Service
            var encConnStrMySqlEmployees = Configuration.GetConnectionString("MySqlEmployees(Azure)");
            var connStrMySqlEmployees = AesCryptoUtil.Decrypt(encConnStrMySqlEmployees);
            services.AddDbContext<EmployeesContext>(builder =>                   
                builder.UseMySQL(connStrMySqlEmployees)
            );
            EnsureDatabaseExists<EmployeesContext>(connStrMySqlEmployees);

            services.AddTransient<EmployeesDataSeeder>();

            var encConnStrMySqlSts = Configuration.GetConnectionString("MySqlSts(Azure)");
            var connStrMySqlSts = AesCryptoUtil.Decrypt(encConnStrMySqlSts);
            services.AddDbContext<stsContext>(builder =>                   
                builder.UseMySQL(connStrMySqlSts)
            );
            EnsureDatabaseExists<stsContext>(connStrMySqlSts);

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
            services.AddScoped<IRepository<Aspnetusers>, AspNetUsersRepository>();
        }

        private static void EnsureDatabaseExists<T>(string connectionString) 
        where T : DbContext, new()
        {
            var builder = new DbContextOptionsBuilder<T>();
            if (typeof(T) == typeof(EmployeesContext)) {
                builder.UseMySQL(connectionString);
            }
            else if (typeof(T) == typeof(stsContext)) {
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            foreach (var s in securitySettings.AllowedCorsOrigins) {
                logger.LogInformation(string.Format("{0}: {1}", "AllowedCorsOrigins", s));
            }
            logger.LogInformation(string.Format("{0}: {1}", "StsAuthority", securitySettings.StsAuthority));
            logger.LogInformation(string.Format("{0}: {1}", "ApiName", securitySettings.ApiName));

            // if (env.IsDevelopment())
            // {
            //     app.UseDeveloperExceptionPage();
            // }

            app.UseCors(EmpWebOrigins);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCaching();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
