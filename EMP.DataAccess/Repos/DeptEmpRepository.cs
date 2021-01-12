using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;

namespace EMP.DataAccess.Repos
{
    public class DeptEmpRepository : IRepository<VwDeptEmpCurrent>
    {
        private EmployeesContext _context;

        public DeptEmpRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<VwDeptEmpCurrent>> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            return await TaskConstants<IEnumerable<VwDeptEmpCurrent>>.NotImplemented;
        }

        public async Task<VwDeptEmpCurrent> GetAsync(string id)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }
        public async Task<VwDeptEmpCurrent> PutAsync(string id, VwDeptEmpCurrent updateRequest)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }

        public async Task<VwDeptEmpCurrent> PostAsync(VwDeptEmpCurrent createRequest)
        {
            string storedProcName = "sp_insert_dept_emp";
            // DbParameter outputParam;
            VwDeptEmpCurrent spResults = null;
            await _context
                .LoadStoredProc(storedProcName)
                .WithSqlParam("empNo", createRequest.EmpNo)
                .WithSqlParam("deptNo", createRequest.DeptNo)
                // .WithSqlParam("result", (dbParam) =>
                // {
                //     dbParam.Direction = System.Data.ParameterDirection.Output;
                //     dbParam.DbType = System.Data.DbType.String;
                //     outputParam = dbParam;
                // })
                .ExecuteStoredProcAsync(_context, (handler) => {
                    spResults = handler.ReadToList<VwDeptEmpCurrent>().FirstOrDefault();
                });

            return spResults;
        }

        public async Task<VwDeptEmpCurrent> DeleteAsync(string id)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }
    }
}
