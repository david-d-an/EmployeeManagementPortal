using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.EFCore;

namespace EMP.DataAccess.Repos
{
    public class TitleRepository : IRepository<VwTitlesCurrent>
    {
        private EmployeesContext _context;

        public TitleRepository(EmployeesContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<VwTitlesCurrent>> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            return await TaskConstants<IEnumerable<VwTitlesCurrent>>.NotImplemented;
        }

        public async Task<VwTitlesCurrent> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwTitlesCurrent>(null);

            return await TaskConstants<VwTitlesCurrent>.NotImplemented;
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
                .WithSqlParam("title", createRequest.Title)
                // .WithSqlParam("result", (dbParam) =>
                // {
                //     dbParam.Direction = System.Data.ParameterDirection.Output;
                //     dbParam.DbType = System.Data.DbType.String;
                //     outputParam = dbParam;
                // })
                .ExecuteStoredProcAsync(_context, (handler) => {
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