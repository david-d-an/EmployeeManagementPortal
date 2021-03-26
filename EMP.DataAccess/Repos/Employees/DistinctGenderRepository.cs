using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMP.Common.Tasks;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using Em = EMP.Data.Models.Employees;

namespace EMP.DataAccess.Repos.Employees
{
    public class DistinctGenderRepository : IRepository<Em.DistinctGenders>
    {
        private EmployeesContext _context;

        public DistinctGenderRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<Em.DistinctGenders> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Em.Employees> dbSet =  _context.Employees;
            IQueryable<Em.Employees> query = dbSet.AsNoTracking();

            var result = query
                .Where(r => r.Gender != null)
                .Select(g => new Em.DistinctGenders {
                    Gender = g.Gender
                }).Distinct();

            return result.ToList();
        }

        public async Task<Em.DistinctGenders> GetAsync(string id)
        {
            return await TaskConstants<Em.DistinctGenders>.NotImplemented;
        }

        public async Task<Em.DistinctGenders> PutAsync(string id, Em.DistinctGenders updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Em.DistinctGenders>(null);

            return await TaskConstants<Em.DistinctGenders>.NotImplemented;
        }

        public async Task<Em.DistinctGenders> PostAsync(Em.DistinctGenders createRequest)
        {
            return await TaskConstants<Em.DistinctGenders>.NotImplemented;
        }

        public async Task<Em.DistinctGenders> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Em.DistinctGenders>(null);

            return await TaskConstants<Em.DistinctGenders>.NotImplemented;
        }
    }
}