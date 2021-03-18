using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models.Employees;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;
using EMP.DataAccess.Repos.Extension;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class TitleRepository : IRepository<VwTitlesCurrent>
    {
        private EmployeesContext _context;

        public TitleRepository(EmployeesContext context)
        {
            this._context = context;
        }
        
        public IEnumerable<VwTitlesCurrent> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<VwTitlesCurrent> dbSet =  _context.VwTitlesCurrent;

            IQueryable<VwTitlesCurrent> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<VwTitlesCurrent> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwTitlesCurrent>(null);

            IQueryable<VwTitlesCurrent> query = _context.VwTitlesCurrent
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VwTitlesCurrent> PutAsync(string id, VwTitlesCurrent updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwTitlesCurrent>(null);

            return await TaskConstants<VwTitlesCurrent>.NotImplemented;
        }

        public async Task<VwTitlesCurrent> PostAsync(VwTitlesCurrent createRequest)
        {
            string storedProcName = "sp_insert_title";
            // DbParameter outputParam;
            VwTitlesCurrent spResults = null;
            await _context
                .LoadStoredProc(storedProcName)
                .WithSqlParam("empNo", createRequest.EmpNo)
                .WithSqlParam("title_new", createRequest.Title)
                // .WithSqlParam("result", (dbParam) =>
                // {
                //     dbParam.Direction = System.Data.ParameterDirection.Output;
                //     dbParam.DbType = System.Data.DbType.String;
                //     outputParam = dbParam;
                // })
                .ExecuteStoredProcAsync(_context, (handler) => {
                    bool nr = handler.NextResult();
                    spResults = handler.ReadToList<VwTitlesCurrent>().FirstOrDefault();
                });

            return spResults;
        }

        public async Task<VwTitlesCurrent> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwTitlesCurrent>(null);

            return await TaskConstants<VwTitlesCurrent>.NotImplemented;
        }

    }
}