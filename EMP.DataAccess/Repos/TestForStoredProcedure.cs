using System.Collections.Generic;
using System.Data.Common;
using EMP.Data.Models.Employees;
// using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos 
{
    public class TestForStoredProcedure {
        private EmployeesContext _context;

        public TestForStoredProcedure(EmployeesContext context)
        {
            this._context = context;
        }

        public TestForStoredProcedure() {
                        string storedProcName = "test_proc";
            var database = _context.Database.GetDbConnection().Database;
            if (database != null)
                storedProcName = $"{database}.{storedProcName}";
            else
                storedProcName = $"{storedProcName}";

            DbParameter outputParam;
            IList<Departments> fooResults;
            IList<Departments> barResults;
            _context.LoadStoredProc(storedProcName)
            .WithSqlParam("empNo", 11)
            .WithSqlParam("salary", (dbParam) =>
            {
                dbParam.Direction = System.Data.ParameterDirection.Output;
                dbParam.DbType = System.Data.DbType.Int32;
                outputParam = dbParam;
            })
            .ExecuteStoredProc(_context, (handler) => {
                fooResults = handler.ReadToList<Departments>();
                handler.NextResult();
                barResults = handler.ReadToList<Departments>();
            });

        }
    }
}
