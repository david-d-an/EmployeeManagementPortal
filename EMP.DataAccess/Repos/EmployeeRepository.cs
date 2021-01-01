using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private EmployeesContext _context;

        public EmployeeRepository(EmployeesContext context)
       {
           this._context = context;
       }

        public async Task<IEnumerable<Employees>> GetAsync()
        {
            // throw new System.NotImplementedException();
            return await TaskConstants<IEnumerable<Employees>>.NotImplemented;
        }

        public async Task<Employees> GetAsync(int id)
        {
            // throw new System.NotImplementedException();
            return await TaskConstants<Employees>.NotImplemented;
        }

        public async Task<Employees> PutAsync(int id, Employees employeeUpdateRequest)
        {
            // throw new System.NotImplementedException();
            return await TaskConstants<Employees>.NotImplemented;
        }

        public async Task<Employees> PostAsync(Employees employeeCreateRequest)
        {
            // throw new System.NotImplementedException();
            return await TaskConstants<Employees>.NotImplemented;
        }

        public async Task<Employees> DeleteAsync(int empNo)
        {
            // throw new System.NotImplementedException();
            return await TaskConstants<Employees>.NotImplemented;
        }

    }
}