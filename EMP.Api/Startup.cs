using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Data.Repos;
using EMP.Common.Security;
using EMP.DataAccess.Repos;
using EMP.DataDataAccess.Context;
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

namespace EMP.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cryptoUtil = new AesCryptoUtil();

            services.AddControllers();

            // Make MySql Connection Service
            var encConnStrMySql = Configuration.GetConnectionString("MySqlConnection");
            var connStrMySql = cryptoUtil.Decrypt(encConnStrMySql);
            services.AddDbContext<EmployeesContext>(builder =>                   
                builder.UseMySQL(connStrMySql)
            );
            EnsureDatabaseExists<EmployeesContext>(connStrMySql);

            services.AddScoped<IRepository<Departments>, DepartmentsRepository>();
            services.AddScoped<IRepository<VwDeptEmpCurrent>, DeptEmpRepository>();
            services.AddScoped<IRepository<VwDeptManagerCurrent>, DeptManagerCurrentRepository>();
            services.AddScoped<IRepository<VwEmpDetails>, EmployeeDetailRepository>();
            services.AddScoped<IRepository<Employees>, EmployeeRepository>();
            services.AddScoped<IRepository<VwTitlesCurrent>, TitleRepository>();
            services.AddScoped<IRepository<VwSalariesCurrent>, SalaryRepository>();
            services.AddScoped<IRepository<Departments>, DepartmentsRepository>();
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
