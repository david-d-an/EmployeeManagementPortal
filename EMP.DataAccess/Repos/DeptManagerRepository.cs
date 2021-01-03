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

        public async Task<DeptManager> GetAsync(string deptNo)
        {            
            return await _context.DeptManager
                        .Where(i => i.DeptNo == deptNo)
                        .FirstOrDefaultAsync();
        }

        public async Task<DeptManager> PostAsync(DeptManager deptManagerCreateRequest)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }

        public async Task<DeptManager> PutAsync(string deptNo, DeptManager deptManagerUpdateRequest)
        {
            return await TaskConstants<DeptManager>.NotImplemented;
        }
    }
}