using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Data.Repos;
using EMP.Common.Security;
using EMP.DataAccess.Repos;
using EMP.DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EMP.Data.Models;
using Microsoft.Net.Http.Headers;

namespace EMP.Api
{
    public class Startup
    {
        private readonly string EmpWebOrigins = "EMP.Web";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: EmpWebOrigins, builder =>
                {
                    builder
                    .WithOrigins(
                        "http://localhos:5000",
                        "https://localhost:5001"
                    )
                    .WithMethods("GET", "PUT", "POST", "DELETE")
                    // .AllowAnyHeader();
                    .WithHeaders(HeaderNames.AccessControlAllowHeaders, "Content-Type");
                });
            });

            var cryptoUtil = new AesCryptoUtil();

            services.AddControllers();

            // Make MySql Connection Service
            var encConnStrMySql = Configuration.GetConnectionString("MySqlConnection(Azure)");
            var connStrMySql = cryptoUtil.Decrypt(encConnStrMySql);

            services.AddDbContext<EmployeesContext>(builder =>                   
                builder.UseMySQL(connStrMySql)
            );
            EnsureDatabaseExists<EmployeesContext>(connStrMySql);

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
