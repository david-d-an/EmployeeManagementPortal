using System.Collections.Generic;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMP.Common.Tasks;
using System.Data;

namespace EMP.DataAccess.Repos
{
    public class DepartmentsRepository : IRepository<Departments>
    {
        private EmployeesContext _context;

        public DepartmentsRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<Departments>> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            IQueryable<Departments> result = 
                from d in this._context.Departments
                select d;

            return await result.ToListAsync();
        }

        public async Task<Departments> GetAsync(string id)
        {
            IQueryable<Departments> result = 
                from d in this._context.Departments
                where d.DeptNo == id
                select d;

            return await result.FirstOrDefaultAsync();
        }

        public async Task<Departments> PutAsync(string id, Departments updateRequest)
        {
            IQueryable<Departments> result = 
                from d in this._context.Departments
                where d.DeptNo == id
                select d;

            var dept = await result.FirstAsync();
            dept.DeptName = updateRequest.DeptName;

            _context.Entry(dept).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return dept;
        }

        public async Task<Departments> PostAsync(Departments createRequest)
        {
            return await TaskConstants<Departments>.NotImplemented;
        }

        public async Task<Departments> DeleteAsync(string id)
        {
            return await TaskConstants<Departments>.NotImplemented;
        }
    }
}