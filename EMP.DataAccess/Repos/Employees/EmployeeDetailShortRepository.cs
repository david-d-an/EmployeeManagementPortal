using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EMP.Data.Repos;
using EMP.Data.Models.Employees;
using EMP.DataAccess.Context;
using EMP.DataAccess.Repos.Extension;
using EMP.Common.Tasks;

namespace EMP.DataAccess.Repos.Employees
{
    public class EmployeeDetailShortRepository : IRepository<VwEmpDetailsShort>
    {
        private EmployeesContext _context;

        public EmployeeDetailShortRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public IEnumerable<VwEmpDetailsShort> GetAsync(
            object parameters = null, 
            int? pageNum = null, 
            int? pageSize = null)
        {
            DbSet<VwEmpDetailsShort> dbSet =  _context.VwEmpDetailsShort;
            IQueryable<VwEmpDetailsShort> query = dbSet.AsNoTracking();

            if (parameters != null) {
                var param = (List<KeyValuePair<string, string>>)parameters;

                foreach(KeyValuePair<string, string> kv in param) {
                    if (kv.Value == null)
                        continue;

                    if (kv.Key == "firstName") {
                        query = query.Where(i => i.FirstName.Contains(kv.Value));
                    }
                    else if (kv.Key == "lastName") {
                        query = query.Where(i => i.LastName.Contains(kv.Value));
                    }
                    else if (kv.Key == "salaryMin") {
                        decimal s;
                        if (decimal.TryParse(kv.Value, out s)){
                            query = query.Where(i => i.Salary >= s);
                        }
                    }
                    else if (kv.Key == "salaryMax") {
                        decimal s;
                        if (decimal.TryParse(kv.Value, out s)){
                            query = query.Where(i => i.Salary <= s);
                        }
                    }
                    else if (kv.Key == "title") {
                        query = query.Where(i => i.Title.Contains(kv.Value));
                    }
                    else if (kv.Key == "deptName") {
                        query = query.Where(i => i.DeptName.Contains(kv.Value));
                    }
                }
            }

            if ((pageNum??0) >  0 && (pageSize??0) > 0) {
                query = query
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return query.ToEnumerable();
        }

        public async Task<VwEmpDetailsShort> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwEmpDetailsShort>(null);

            IQueryable<VwEmpDetailsShort> query = _context.VwEmpDetailsShort
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VwEmpDetailsShort> PutAsync(string id, VwEmpDetailsShort updateRequest)
        {
            return await TaskConstants<VwEmpDetailsShort>.NotImplemented;
        }

        public async Task<VwEmpDetailsShort> PostAsync(VwEmpDetailsShort createRequest)
        {
            return await TaskConstants<VwEmpDetailsShort>.NotImplemented;
        }

        public async Task<VwEmpDetailsShort> DeleteAsync(string id)
        {
            return await TaskConstants<VwEmpDetailsShort>.NotImplemented;
        }



    }
}