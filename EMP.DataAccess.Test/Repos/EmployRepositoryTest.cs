
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
using EMP.Common.Security;

namespace EMP.DataAccess.Repos
{
    public class EmployRepositoryTest {
        private string connStrMySql;
        private DbContextOptionsBuilder<EmployeesContext> dbOptionsbuilder;
        private EmployeesContext context;

        public EmployRepositoryTest()
        {
            string mySqlEmployeesConnStr = 
                "0nmBbjjPY3PGbA6j+7Ul0Od1V+u8TMv8E1oQrIvrJTqG8JHkQaQ40CGThX5pKBsAVir1FefOpPPZpgsFZLA6eO8fRum5wnZkcxGWw9aq0ovHRM0OhKYf1GS0YK2slp1jMaKpA0HDylDsswiZ3CByr0cUGPwqSEn04hJAd3FXfbWPpGlUZ4zQz0MO4avuEA1Z",
            connStrMySql = AesCryptoUtil.Decrypt(mySqlEmployeesConnStr);
            dbOptionsbuilder = new DbContextOptionsBuilder<EmployeesContext>().UseMySQL(connStrMySql);
            context = new EmployeesContext(dbOptionsbuilder.Options);
        }

        [Fact]
        public void ShouldPass() {
            Assert.True(0 == 0);
        }
    }

}