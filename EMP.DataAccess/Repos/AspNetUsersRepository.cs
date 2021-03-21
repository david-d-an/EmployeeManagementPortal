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
    public class AspnetUsersRepository : IRepository<Aspnetusers>
    {
        private StsContext _context;

        public AspnetUsersRepository(StsContext context)
        {
            this._context = context;
        }
        
        public IEnumerable<Aspnetusers> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Aspnetusers> dbSet =  _context.Aspnetusers;
            IQueryable<Aspnetusers> query = dbSet.AsNoTracking()
                .Select(r => new Aspnetusers {
                    Id = r.Id,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber,
                    UserName = r.UserName
                });

            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<Aspnetusers> GetAsync(string id)
        {
            IQueryable<Aspnetusers> result = _context.Aspnetusers
                .Where(r => r.UserName == id)
                .Select(r => new Aspnetusers {
                    Id = r.Id,
                    Email = r.Email,
                    PhoneNumber = r.PhoneNumber,
                    UserName = r.UserName
                });

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Aspnetusers> PutAsync(string id, Aspnetusers updateRequest)
        {
            return await TaskConstants<Aspnetusers>.NotImplemented;
        }

        public async Task<Aspnetusers> PostAsync(Aspnetusers createRequest)
        {
            return await TaskConstants<Aspnetusers>.NotImplemented;
        }

        public async Task<Aspnetusers> DeleteAsync(string id)
        {
            return await TaskConstants<Aspnetusers>.NotImplemented;
        }

    }
}