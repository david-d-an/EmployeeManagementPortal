using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;
using EMP.DataAccess.Repos.Extension;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class DeptEmpRepository : IRepository<VwDeptEmpCurrent>
    {
        private EmployeesContext _context;

        public DeptEmpRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<VwDeptEmpCurrent> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            DbSet<VwDeptEmpCurrent> dbSet =  _context.VwDeptEmpCurrent;

            IQueryable<VwDeptEmpCurrent> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
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
