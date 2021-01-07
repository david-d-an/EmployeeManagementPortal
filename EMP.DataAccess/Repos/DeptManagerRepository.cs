using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class DeptManagerRepository : IDeptManagerRepository
    {
        private EmployeesContext _context;

        public DeptManagerRepository(EmployeesContext context)
        {
            this._context = context;
        }
        public async Task<IEnumerable<DeptManager>> GetAsync()
        {
            IQueryable<DeptManager> deptManagers  = _context.DeptManager;
            return await deptManagers.ToListAsync();
        }

        public async Task<DeptManager> GetAsync(string id)
        {            
            return await _context.DeptManager
                        .Where(i => i.DeptNo == id)
                        .FirstOrDefaultAsync();
        }

        public async Task<DeptManager> PutAsync(string id, DeptManager updateRequest)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }

        public async Task<DeptManager> PostAsync(DeptManager createRequest)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }

        public async Task<DeptManager> DeleteAsync(string id)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }
    }
}