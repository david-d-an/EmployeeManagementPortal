using System.Linq;
using EMP.Common.Security;
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
            string mySqlEmployeesConnStr = 
                "0nmBbjjPY3PGbA6j+7Ul0Od1V+u8TMv8E1oQrIvrJTqG8JHkQaQ40CGThX5pKBsAVir1FefOpPPZpgsFZLA6eO8fRum5wnZkcxGWw9aq0ovHRM0OhKYf1GS0YK2slp1jMaKpA0HDylDsswiZ3CByr0cUGPwqSEn04hJAd3FXfbWPpGlUZ4zQz0MO4avuEA1Z";
            connStrMySql = AesCryptoUtil.Decrypt(mySqlEmployeesConnStr);
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
