using EMP.Data.Repos;
using EMP.Data.Models;
using EMP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.DataAccess.Repos.Extension;
using EMP.Common.Tasks;

namespace EMP.DataAccess.Repos
{
    public class EmployeeDetailShortRepository : IRepository<VwEmpDetailsShort>
    {
        private EmployeesContext _context;

        public EmployeeDetailShortRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<VwEmpDetailsShort> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            pageNum = 1;
            pageSize = 2;
            DbSet<VwEmpDetailsShort> dbSet =  _context.VwEmpDetailsShort;

            IQueryable<VwEmpDetailsShort> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<VwEmpDetailsShort> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwEmpDetailsShort>(null);

            IQueryable<VwEmpDetailsShort> query = _context.VwEmpDetailsShort
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VwEmpDetailsShort> PutAsync(string id, VwEmpDetailsShort updateRequest)
        {
            return await TaskConstants<VwEmpDetailsShort>.NotImplemented;
        }

        public async Task<VwEmpDetailsShort> PostAsync(VwEmpDetailsShort createRequest)
        {
            return await TaskConstants<VwEmpDetailsShort>.NotImplemented;
        }

        public async Task<VwEmpDetailsShort> DeleteAsync(string id)
        {
            return await TaskConstants<VwEmpDetailsShort>.NotImplemented;
        }



    }
}