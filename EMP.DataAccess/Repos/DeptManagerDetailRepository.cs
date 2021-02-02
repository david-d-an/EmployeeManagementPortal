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
    public class DeptManagerDetailRepository : IRepository<VwDeptManagerDetail> {
        private EmployeesContext _context;

        public DeptManagerDetailRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<VwDeptManagerDetail> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<VwDeptManagerDetail> dbSet =  _context.VwDeptManagerDetail;

            IQueryable<VwDeptManagerDetail> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<VwDeptManagerDetail> GetAsync(string id)
        {
            if (id == null || id.Trim() == string.Empty)
                return await Task.FromResult<VwDeptManagerDetail>(null);

            IQueryable<VwDeptManagerDetail> query = _context.VwDeptManagerDetail
                .Where(i => i.DeptNo == id);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VwDeptManagerDetail> PutAsync(string id, VwDeptManagerDetail updateRequest)
        {
            return await TaskConstants<VwDeptManagerDetail>.NotImplemented;
        }

        public async Task<VwDeptManagerDetail> PostAsync(VwDeptManagerDetail createRequest)
        {
            return await TaskConstants<VwDeptManagerDetail>.NotImplemented;
        }

        public async Task<VwDeptManagerDetail> DeleteAsync(string id)
        {
            return await TaskConstants<VwDeptManagerDetail>.NotImplemented;
        }
    }
}
