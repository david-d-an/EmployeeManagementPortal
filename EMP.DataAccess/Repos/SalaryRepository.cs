using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class SalaryRepository : IRepository<VwSalariesCurrent>
    {
        public EmployeesContext _context;

        public SalaryRepository(EmployeesContext context)
        {
            this._context = context;   
        }

        public async Task<IEnumerable<VwSalariesCurrent>> GetAsync()
        {
            return await TaskConstants<IEnumerable<VwSalariesCurrent>>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwSalariesCurrent>(null);

            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> PostAsync(VwSalariesCurrent createRequest)
        {
            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> PutAsync(string id, VwSalariesCurrent updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwSalariesCurrent>(null);

            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwSalariesCurrent>(null);

            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }
    }
}