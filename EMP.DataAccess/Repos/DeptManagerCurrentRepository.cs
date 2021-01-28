using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Models.Mapped;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;
using EMP.DataAccess.Repos.Extension;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class DeptManagerCurrentRepository : IRepository<VwDeptManagerCurrent>
    {
        private EmployeesContext _context;

        public DeptManagerCurrentRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<VwDeptManagerCurrent> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            DbSet<VwDeptManagerCurrent> dbSet =  _context.VwDeptManagerCurrent;

            IQueryable<VwDeptManagerCurrent> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<VwDeptManagerCurrent> GetAsync(string id)
        {            
            return await _context.VwDeptManagerCurrent
                        .Where(i => i.DeptNo == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<VwDeptManagerCurrent> PostAsync(VwDeptManagerCurrent createRequest)
        {
            string storedProcName = "sp_insert_dept_manager";
            // DbParameter outputParam;
            VwDeptManagerCurrent spResults = null;
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
                    spResults = handler.ReadToList<VwDeptManagerCurrent>().FirstOrDefault();
                });

            return spResults;
        }

        public async Task<VwDeptManagerCurrent> PutAsync(string id, VwDeptManagerCurrent updateRequest)
        {
            return await TaskConstants<VwDeptManagerCurrent>.NotImplemented;
        }

        public async Task<VwDeptManagerCurrent> DeleteAsync(string id)
        {
            return await TaskConstants<VwDeptManagerCurrent>.NotImplemented;
        }
    }
}