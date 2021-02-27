using System.Linq;
using EMP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EMP.DataAccess.Context
{
    public class EmployeesContextShould
    {
        private string connStrMySql;
        private DbContextOptionsBuilder<EmployeesContext> dbOptionsbuilder;
        private EmployeesContext context;

        public EmployeesContextShould()
        {
            connStrMySql = "Server=mycompany6921.mysql.database.azure.com; Port=3306; Database=employees; Uid=appuser@mycompany6921; Pwd=Soil9303; SslMode=Preferred;";

            dbOptionsbuilder = new DbContextOptionsBuilder<EmployeesContext>().UseMySQL(connStrMySql);
            context = new EmployeesContext(dbOptionsbuilder.Options);
        }

        [Fact]
        public void ConnectToDepartmentsTable()
        {
            Assert.NotNull(context.Departments);
            var departmentCount = context.Departments.Count();
            Assert.True(departmentCount >= 0);
            
        }

        [Fact]
        public void ConnectToDeptManagerTable()
        {
            Assert.NotNull(context.DeptManager);
            var deptManagerCount = context.DeptManager.Count();
            Assert.True(deptManagerCount >= 0);            
        }

        [Fact]
        public void ConnectToEmployeesTable()
        {
            Assert.NotNull(context.Employees);
            var employeesCount = context.Employees.Count();
            Assert.True(employeesCount >= 0);
        }

        [Fact]
        public void ConnectToSalariesTable()
        {
            Assert.NotNull(context.Salaries);
            var salariesCount = context.Salaries.Count();
            Assert.True(salariesCount >= 0);
        }
    }
}
