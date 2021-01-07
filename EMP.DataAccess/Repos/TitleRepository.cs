using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class TitleRepository : ITitleRepository
    {
        private EmployeesContext _context;

        public TitleRepository(EmployeesContext context)
        {
            this._context = context;
        }
        
        public async Task<IEnumerable<VwTitlesCurrent>> GetAsync()
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
            return await TaskConstants<VwTitlesCurrent>.NotImplemented;
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