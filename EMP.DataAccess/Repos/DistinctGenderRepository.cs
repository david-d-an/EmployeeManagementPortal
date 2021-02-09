using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class DistinctGenderRepository : IRepository<DistinctGenders>
    {
        private EmployeesContext _context;

        public DistinctGenderRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<DistinctGenders> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Employees> dbSet =  _context.Employees;
            IQueryable<Employees> query = dbSet.AsNoTracking();

            var result = query
                .Where(r => r.Gender != null)
                .Select(g => new DistinctGenders {
                    Gender = g.Gender
                }).Distinct();

            return result.ToList();
        }

        public async Task<DistinctGenders> GetAsync(string id)
        {
            return await TaskConstants<DistinctGenders>.NotImplemented;
        }

        public async Task<DistinctGenders> PutAsync(string id, DistinctGenders updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<DistinctGenders>(null);

            return await TaskConstants<DistinctGenders>.NotImplemented;
        }

        public async Task<DistinctGenders> PostAsync(DistinctGenders createRequest)
        {
            return await TaskConstants<DistinctGenders>.NotImplemented;
        }

        public async Task<DistinctGenders> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<DistinctGenders>(null);

            return await TaskConstants<DistinctGenders>.NotImplemented;
        }
    }
}