using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models.Employees;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using EMP.DataAccess.Repos.Extension;
using Microsoft.EntityFrameworkCore;
using EMP.Common.Data;
using System;

namespace EMP.DataAccess.Repos
{
    public class EmployeeRepository : IRepository<Employees>
    {
        private EmployeesContext _context;

        public EmployeeRepository(EmployeesContext context)
       {
           this._context = context;
       }

        public IEnumerable<Employees> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<Employees> dbSet =  _context.Employees;

            IQueryable<Employees> query = dbSet.AsNoTracking();
            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<Employees> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Employees>(null);

            IQueryable<Employees> query = _context.Employees
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Employees> PutAsync(string id, Employees updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Employees>(null);

            Employees employee = await _context.Employees.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();

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



        public async Task<Employees> PostAsync(Employees createRequest)
        {
            // TO DO: Need to finish this to complete EmployeeDetail/POST
            // return await TaskConstants<Employees>.NotImplemented;

            _context.Entry(createRequest).State = EntityState.Added;
            await _context.SaveChangesAsync();
            return createRequest;
        }

        public async Task<Employees> DeleteAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<Employees>(null);

            return await TaskConstants<Employees>.NotImplemented;
        }

        // private void foo() {
        //     string sortBy = "id";
        //     Expression<Func<Employees, object>> sortExpression;
        //     switch (sortBy)
        //     {
        //         case "Id":
        //             sortExpression = (x => x.EmpNo);
        //             break;
        //         case "CreateDate":
        //             sortExpression = (x => x.BirthDate);
        //             break;
        //         case "Dealer.DealerName":
        //             sortExpression = (x => x.FirstName);
        //             break;
        //         case "ClaimType.ClaimTypeName":
        //             sortExpression = (x => x.LastName);
        //             break;
        //         case "ClaimReason":
        //             sortExpression = (x => x.HireDate);
        //             break;
        //         case "ClaimStatus.ClaimStatusName":
        //             sortExpression = (x => x.Salaries);
        //             break;
        //         default:
        //             sortExpression = (x => x.Salaries);
        //             break;
        //     }
        // }

    }
}