using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class EmployeeRepository : IRepository<Employees>
    {
        private EmployeesContext _context;

        public EmployeeRepository(EmployeesContext context)
       {
           this._context = context;
       }

        public async Task<IEnumerable<Employees>> GetAsync()
        {
            return await TaskConstants<IEnumerable<Employees>>.NotImplemented;
        }

        public async Task<Employees> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Employees>(null);

            return await TaskConstants<Employees>.NotImplemented;
        }

        public async Task<Employees> PutAsync(string id, Employees updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Employees>(null);

            return await TaskConstants<Employees>.NotImplemented;
        }

        public async Task<Employees> PostAsync(Employees createRequest)
        {
            return await TaskConstants<Employees>.NotImplemented;
        }

        public async Task<Employees> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Employees>(null);

            return await TaskConstants<Employees>.NotImplemented;
        }

    }
}