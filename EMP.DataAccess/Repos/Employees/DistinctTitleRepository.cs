using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMP.Common.Tasks;
using EMP.Data.Models.Employees;
using EMP.Data.Repos;
using EMP.DataAccess.Context;

namespace EMP.DataAccess.Repos.Employees
{
    public class DistinctTitleRepository : IRepository<DistinctTitles>
    {
        private EmployeesContext _context;

        public DistinctTitleRepository(EmployeesContext context)
        {
            this._context = context;
        }
        
        public IEnumerable<DistinctTitles> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<VwTitlesCurrent> dbSet =  _context.VwTitlesCurrent;
            IQueryable<VwTitlesCurrent> query = dbSet.AsNoTracking();

            // var result = query
            //     .Where(r => r.Title != null)
            //     .GroupBy(n => new { n.Title })
            //     .Select(g => new DistinctTitles {
            //         Title = g.FirstOrDefault().ToString()
            //     });

            var result = query
                .Where(r => r.Title != null)
                .Select(g => new DistinctTitles {
                    Title = g.Title
                }).Distinct();

            return result.ToList();
        }

        public async Task<DistinctTitles> GetAsync(string id)
        {
            return await TaskConstants<DistinctTitles>.NotImplemented;
        }

        public async Task<DistinctTitles> PutAsync(string id, DistinctTitles updateRequest)
        {
            return await TaskConstants<DistinctTitles>.NotImplemented;
        }

        public async Task<DistinctTitles> PostAsync(DistinctTitles createRequest)
        {
            return await TaskConstants<DistinctTitles>.NotImplemented;
        }

        public async Task<DistinctTitles> DeleteAsync(string id)
        {
            return await TaskConstants<DistinctTitles>.NotImplemented;
        }

    }
}