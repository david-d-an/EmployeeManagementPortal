
using System.Collections.Generic;
using System.Linq;
using EMP.Data.Repos;
using EMP.Data.Models;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EMP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;


namespace EMP.DataAccess.Repos
{
    public class EmployRepositoryTest {
        private string connStrMySql;
        private DbContextOptionsBuilder<EmployeesContext> dbOptionsbuilder;
        private EmployeesContext context;

        public EmployRepositoryTest()
        {
            connStrMySql = "Server=mycompany6921.mysql.database.azure.com; Port=3306; Database=employees; Uid=appuser@mycompany6921; Pwd=Soil9303; SslMode=Preferred;";
            dbOptionsbuilder = new DbContextOptionsBuilder<EmployeesContext>().UseMySQL(connStrMySql);
            context = new EmployeesContext(dbOptionsbuilder.Options);
        }

        [Fact]
        public void ShouldPass() {
            Assert.True(0 == 0);
        }
    }

}