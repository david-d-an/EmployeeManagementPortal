using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Models.Mapped;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class DeptManagerCurrentRepository : IDeptManagerCurrentRepository
    {
        private EmployeesContext _context;

        public DeptManagerCurrentRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<VwDeptManagerCurrent>> GetAsync()
        {
            IQueryable<VwDeptManagerCurrent> deptManagers  = _context.VwDeptManagerCurrent;
            return await deptManagers.ToListAsync();
        }

        public async Task<VwDeptManagerCurrent> GetAsync(string deptNo)
        {            
            return await _context.VwDeptManagerCurrent
                        .Where(i => i.DeptNo == deptNo)
                        .FirstOrDefaultAsync();
        }

        public async Task<VwDeptManagerCurrent> PostAsync(VwDeptManagerCurrent deptManagerCreateRequest)
        {
            return await TaskConstants<VwDeptManagerCurrent>.NotImplemented;
        }

        public async Task<VwDeptManagerCurrent> PutAsync(string deptNo, VwDeptManagerCurrent deptManagerUpdateRequest)
        {
            return await TaskConstants<VwDeptManagerCurrent>.NotImplemented;
        }
    }
}