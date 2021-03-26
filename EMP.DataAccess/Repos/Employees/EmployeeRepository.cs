using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMP.Common.Tasks;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.Repos.Extension;
using Em = EMP.Data.Models.Employees;

namespace EMP.DataAccess.Repos.Employees
{
    public class EmployeeRepository : IRepository<Em.Employees>
    {
        private EmployeesContext _context;

        public EmployeeRepository(EmployeesContext context)
       {
           this._context = context;
       }

        public IEnumerable<Em.Employees> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Em.Employees> dbSet =  _context.Employees;

            IQueryable<Em.Employees> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<Em.Employees> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Em.Employees>(null);

            IQueryable<Em.Employees> query = _context.Employees
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Em.Employees> PutAsync(string id, Em.Employees updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Em.Employees>(null);

            Em.Employees employee = await _context.Employees
                .Where(i => i.EmpNo == empNo)
                .FirstOrDefaultAsync();

            // try {
            //     DataCloneExtension.CopyProperties(updateRequest, employee);
            // } catch(Exception ex) {
            //     var e = ex;                
            // }
            employee.FirstName = updateRequest.FirstName;
            employee.LastName = updateRequest.LastName;
            employee.BirthDate = updateRequest.BirthDate;
            employee .HireDate= updateRequest.HireDate;
            employee.Gender = updateRequest.Gender;

            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return updateRequest;
        }

        public async Task<Em.Employees> PostAsync(Em.Employees createRequest)
        {
            // TO DO: Need to finish this to complete EmployeeDetail/POST
            // return await TaskConstants<Employees>.NotImplemented;

            _context.Entry(createRequest).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return createRequest;
        }

        public async Task<Em.Employees> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Em.Employees>(null);

            return await TaskConstants<Em.Employees>.NotImplemented;
        }
    }
}