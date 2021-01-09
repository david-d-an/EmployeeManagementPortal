using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace EMP.DataAccess.Repos
{
    public class EmployeeRepository : IRepository<Employees>
    {
        private EmployeesContext _context;

        public EmployeeRepository(EmployeesContext context)
       {
           this._context = context;
       }

        public async Task<IEnumerable<Employees>> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            IQueryable<Employees> query = _context.Employees;
            return await query.ToListAsync();
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