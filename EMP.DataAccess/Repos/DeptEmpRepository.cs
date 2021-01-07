using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;

namespace EMP.DataAccess.Repos
{
    public class DeptEmpRepository : IDeptEmpRepository
    {
        private EmployeesContext _context;

        public DeptEmpRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<VwDeptEmpCurrent>> GetAsync()
        {
            return await TaskConstants<IEnumerable<VwDeptEmpCurrent>>.NotImplemented;
        }

        public async Task<VwDeptEmpCurrent> GetAsync(string id)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }

        public async Task<VwDeptEmpCurrent> PostAsync(VwDeptEmpCurrent createRequest)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }

        public async Task<VwDeptEmpCurrent> PutAsync(string id, VwDeptEmpCurrent updateRequest)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }

        public async Task<VwDeptEmpCurrent> DeleteAsync(string id)
        {
            return await TaskConstants<VwDeptEmpCurrent>.NotImplemented;
        }
    }
}
