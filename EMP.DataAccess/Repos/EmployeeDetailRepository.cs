using EMP.Data.Repos;
using EMP.Data.Models;
using EMP.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using EMP.Common.Tasks;

namespace EMP.DataAccess.Repos
{
    public class EmployeeDetailRepository : IRepository<VwEmpDetails>
    {
        private EmployeesContext _context;

        public EmployeeDetailRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<VwEmpDetails>> GetAsync(int? pageNum = null, int? pageSize = null)
        {
            IQueryable<VwEmpDetails> query = _context.VwEmpDetails;

            if (pageNum != null && pageNum.Value > 0 && 
                pageSize != null && pageSize.Value > 0) {
                query = query
                    .Where(i => i.DeptName == "Customer Service")
                    .Skip((pageNum.Value - 1) * pageSize.Value)
                    .Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<VwEmpDetails> GetAsync(string id)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwEmpDetails>(null);

            IQueryable<VwEmpDetails> query = _context.VwEmpDetails
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<VwEmpDetails> PutAsync(string id, VwEmpDetails updateRequest)
        {
            int empNo;
            if (!int.TryParse(id, out empNo))
                return await Task.FromResult<VwEmpDetails>(null);

            Employees employee = await _context.Employees.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();
            VwSalariesCurrent salary = await _context.VwSalariesCurrent.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();
            VwTitlesCurrent title = await _context.VwTitlesCurrent.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();
            VwDeptEmpCurrent deptEmp = await _context.VwDeptEmpCurrent.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();

            if (title.Title != updateRequest.Title) {
                throw new NotImplementedException();
            }

            if (salary.Salary != updateRequest.Salary) {
                throw new NotImplementedException();
            }

            if (deptEmp.DeptNo != updateRequest.DeptNo) {
                throw new NotImplementedException();
            }

            if (employeeBasicInfoChanged(employee, updateRequest)) {
                employee.BirthDate = updateRequest.BirthDate;
                employee.FirstName = updateRequest.FirstName;
                employee.LastName = updateRequest.LastName;
                employee.Gender = updateRequest.Gender;
                employee.HireDate = updateRequest.HireDate;

                _context.Entry(employee).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return updateRequest;
        }

        private bool employeeBasicInfoChanged(Employees employee, VwEmpDetails employeeDetailUpdateRequest)
        {
            bool result =
                employee.BirthDate != employeeDetailUpdateRequest.BirthDate ||
                employee.FirstName != employeeDetailUpdateRequest.FirstName ||
                employee.LastName != employeeDetailUpdateRequest.LastName ||
                employee.Gender != employeeDetailUpdateRequest.Gender ||
                employee.HireDate != employeeDetailUpdateRequest.HireDate;

            return result;
        }

        public async Task<VwEmpDetails> PostAsync(VwEmpDetails createRequest)
        {
            return await TaskConstants<VwEmpDetails>.NotImplemented;
        }

        public async Task<VwEmpDetails> DeleteAsync(string id)
        {
            return await TaskConstants<VwEmpDetails>.NotImplemented;
        }

    }
}