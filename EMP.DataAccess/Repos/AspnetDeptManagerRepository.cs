using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models.Sts;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;
using EMP.DataAccess.Repos.Extension;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class AspnetDeptManagerRepository : IRepository<AspnetDeptManager>
    {
        private StsContext _context;

        public AspnetDeptManagerRepository(StsContext context)
        {
            this._context = context;
        }
        
        public IEnumerable<AspnetDeptManager> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<AspnetDeptManager> dbSet =  _context.AspnetDeptManager;
            IQueryable<AspnetDeptManager> query = dbSet.AsNoTracking();

            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<AspnetDeptManager> GetAsync(string id)
        {
            int idNum;
            if (!int.TryParse(id, out idNum))
                return await Task.FromResult<AspnetDeptManager>(null);

            IQueryable<AspnetDeptManager> result = _context.AspnetDeptManager
                .Where(r => r.Id == idNum);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<AspnetDeptManager> PutAsync(string id, AspnetDeptManager updateRequest)
        {
            return await TaskConstants<AspnetDeptManager>.NotImplemented;
        }

        public async Task<AspnetDeptManager> PostAsync(AspnetDeptManager createRequest)
        {
            return await TaskConstants<AspnetDeptManager>.NotImplemented;
        }

        public async Task<AspnetDeptManager> DeleteAsync(string id)
        {
            return await TaskConstants<AspnetDeptManager>.NotImplemented;
        }

    }
}