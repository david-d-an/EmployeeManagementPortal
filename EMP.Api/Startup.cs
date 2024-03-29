using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using IdentityServer4.AccessTokenValidation;
using EMP.Common.Security;
using EMP.Data.Repos;
using EMP.Data.Models.Employees;
using EMP.Data.Models.Sts;
using EMP.DataAccess.Repos.Employees;
using EMP.DataAccess.Repos.Sts;
using EMP.DataAccess.Context;
using EMP.Api.Config;
using Serilog;

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
            // TO DO: Create factory to return DB connection by ASYNC
            var encConnStrMySqlEmployees = Configuration.GetConnectionString("MySqlEmployees(Azure)");
            var connStrMySqlEmployees = AesCryptoUtil.Decrypt(encConnStrMySqlEmployees);

            Log.Information($"Connection String: {connStrMySqlEmployees}");

            services.AddDbContext<EmployeesContext>(builder =>                   
                builder.UseMySQL(connStrMySqlEmployees)
            );
            EnsureDatabaseExists<EmployeesContext>(connStrMySqlEmployees);

            services.AddTransient<EmployeesDataSeeder>();

            var encConnStrMySqlSts = Configuration.GetConnectionString("MySqlSts(Azure)");
            var connStrMySqlSts = AesCryptoUtil.Decrypt(encConnStrMySqlSts);
            services.AddDbContext<StsContext>(builder =>                   
                builder.UseMySQL(connStrMySqlSts)
            );
            EnsureDatabaseExists<StsContext>(connStrMySqlSts);

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
            services.AddScoped<IRepository<Aspnetusers>, AspnetUsersRepository>();
            services.AddScoped<IUnitOfWorkEmployees, UnitOfWorkEmployees>();
            services.AddScoped<IUnitOfWorkSts, UnitOfWorkSts>();
        }

        private static void EnsureDatabaseExists<T>(string connectionString) 
        where T : DbContext, new()
        {
            var builder = new DbContextOptionsBuilder<T>();
            if (typeof(T) == typeof(EmployeesContext)) {
                builder.UseMySQL(connectionString);
            }
            else if (typeof(T) == typeof(StsContext)) {
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

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(EmpWebOrigins);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseResponseCaching();
            
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
