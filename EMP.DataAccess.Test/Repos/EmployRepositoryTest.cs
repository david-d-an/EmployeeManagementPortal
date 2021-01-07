
using System.Collections.Generic;
using System.Linq;
using EMP.Data.Repos;
using EMP.Data.Models;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using EMP.DataDataAccess.Context;
using Microsoft.EntityFrameworkCore;


namespace EMP.DataAccess.Repos
{
    public class EmployRepositoryTest {
        private string connStrMySql;
        private DbContextOptionsBuilder<EmployeesContext> dbOptionsbuilder;
        private EmployeesContext context;

        public EmployRepositoryTest()
        {
            connStrMySql = "server=mycompany.cniwlvrfgzdc.us-east-1.rds.amazonaws.com;uid=appuser;password=Soil9303;port=3306;database=employees;";
            dbOptionsbuilder = new DbContextOptionsBuilder<EmployeesContext>().UseMySQL(connStrMySql);
            context = new EmployeesContext(dbOptionsbuilder.Options);
        }

        [Fact]
        public void ShouldPass() {

        }
    }

}