using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMP.Common.Tasks;
using EMP.Data.Models.Employees;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;
using EMP.DataAccess.Repos.Extension;

namespace EMP.DataAccess.Repos.Employees
{
    public class SalaryRepository : IRepository<VwSalariesCurrent>
    {
        public EmployeesContext _context;

        public SalaryRepository(EmployeesContext context)
        {
            this._context = context;   
        }

        public IEnumerable<VwSalariesCurrent> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<VwSalariesCurrent> dbSet =  _context.VwSalariesCurrent;

            IQueryable<VwSalariesCurrent> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<VwSalariesCurrent> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwSalariesCurrent>(null);

            IQueryable<VwSalariesCurrent> query = _context.VwSalariesCurrent
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VwSalariesCurrent> PostAsync(VwSalariesCurrent createRequest)
        {
            string storedProcName = "sp_insert_salary";
            // DbParameter outputParam;
            VwSalariesCurrent spResults = null;
try{
            await _context
                .LoadStoredProc(storedProcName)
                .WithSqlParam("empNo", createRequest.EmpNo)
                .WithSqlParam("salary_new", createRequest.Salary)
                // .WithSqlParam("result", (dbParam) =>
                // {
                //     dbParam.Direction = System.Data.ParameterDirection.Output;
                //     dbParam.DbType = System.Data.DbType.String;
                //     outputParam = dbParam;
                // })
                .ExecuteStoredProcAsync(_context, (handler) => {
                    bool nr = handler.NextResult();
                    spResults = handler.ReadToList<VwSalariesCurrent>().FirstOrDefault();
                });
} catch(Exception ex) {
    throw ex;
}
            return spResults;
        }

        public async Task<VwSalariesCurrent> PutAsync(string id, VwSalariesCurrent updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwSalariesCurrent>(null);

            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwSalariesCurrent>(null);

            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }
    }
}