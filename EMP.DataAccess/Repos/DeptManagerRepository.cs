using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.Repos.Extension;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class DeptManagerRepository : IRepository<DeptManager>
    {
        private EmployeesContext _context;

        public DeptManagerRepository(EmployeesContext context)
        {
            this._context = context;
        }
        public IEnumerable<DeptManager> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<DeptManager> dbSet =  _context.DeptManager;

            IQueryable<DeptManager> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<DeptManager> GetAsync(string id)
        {            
            return await _context.DeptManager
                        .Where(i => i.DeptNo == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<DeptManager> PutAsync(string id, DeptManager updateRequest)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }

        public async Task<DeptManager> PostAsync(DeptManager createRequest)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }

        public async Task<DeptManager> DeleteAsync(string id)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }
    }
}