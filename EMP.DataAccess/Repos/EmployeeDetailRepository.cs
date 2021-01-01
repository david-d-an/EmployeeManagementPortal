using EMP.Data.Repos;
using EMP.Data.Models;
using EMP.DataDataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EMP.DataAccess.Repos
{
    public class EmployeeDetailRepository : IEmployeeDetailRepository
    {
        private EmployeesContext _context;

        public EmployeeDetailRepository(EmployeesContext context)
        {
            this._context = context;
        }

        public async Task<IEnumerable<VwEmpDetails>> GetAsync()
        {
            IQueryable<VwEmpDetails> query = _context.VwEmpDetails;
            return await query.ToListAsync();
        }

        public async Task<VwEmpDetails> GetAsync(int empNo)
        {
            // IQueryable<EmployeeDetail> query =
            //     from e in _context.Employees
            //         .Where(i => i.EmpNo == empNo)
            //     from de in _context.DeptEmp
            //         .Where(i => i.EmpNo == e.EmpNo)
            //         .OrderByDescending(i => i.ToDate).Take(1)
            //     join d in _context.Departments
            //     on de.DeptNo equals d.DeptNo
            //     from dm in _context.DeptManager
            //         .Where(i => i.DeptNo == d.DeptNo)
            //         .OrderByDescending(i => i.ToDate).Take(1)
            //     join emd in _context.Employees
            //     on dm.EmpNo equals emd.EmpNo
            //     from t in _context.Titles
            //         .Where(i => i.EmpNo == e.EmpNo)
            //         .OrderByDescending(i => i.ToDate).Take(1)
            //     from s in _context.Salaries
            //         .Where(i => i.EmpNo == e.EmpNo)
            //         .OrderByDescending(i => i.ToDate).Take(1)
            //     select new EmployeeDetail {
            //         EmpNo = e.EmpNo,
            //         BirthDate = e.BirthDate,
            //         FirstName = e.FirstName,
            //         LastName = e.LastName,
            //         Gender = e.Gender,
            //         HireDate = e.HireDate,
            //         DeptNo = d.DeptNo,
            //         DeptName = d.DeptName,
            //         ManagerEmpNo = emd.EmpNo,
            //         ManagerFirstName = emd.FirstName, 
            //         ManagerLastName = emd.LastName,
            //         Salary = s.Salary,
            //         Title = t.Title
            //     };


            // Employees employee = await _context.Employees
            //     .Where(i => i.EmpNo == empNo)
            //     .FirstOrDefaultAsync();
            
            // DeptEmp deptEmp = await _context.DeptEmp
            //         .Where(i => i.EmpNo == empNo)
            //         .OrderByDescending(i => i.ToDate)
            //         .FirstOrDefaultAsync();

            // Salaries salary = await _context.Salaries
            //         .Where(i => i.EmpNo == empNo)
            //         .OrderByDescending(i => i.ToDate)
            //         .FirstOrDefaultAsync();

            // Titles title = await _context.Titles
            //         .Where(i => i.EmpNo == empNo)
            //         .OrderByDescending(i => i.ToDate)
            //         .FirstOrDefaultAsync();

            // Departments department = await _context.Departments
            //         .Where(i => i.DeptNo == deptEmp.DeptNo)
            //         .FirstOrDefaultAsync();

            // DeptManager deptManager = await _context.DeptManager
            //     .Where(i => i.DeptNo == department.DeptNo)
            //     .FirstOrDefaultAsync();

            // Employees manager = await _context.Employees
            //     .Where(i => i.EmpNo == deptManager.EmpNo)
            //     .FirstOrDefaultAsync();

            // EmployeeDetail employeeDetail = new EmployeeDetail {
            //     EmpNo = employee.EmpNo,
            //     BirthDate = employee.BirthDate,
            //     FirstName = employee.FirstName,
            //     LastName = employee.LastName,
            //     Gender = employee.Gender,
            //     HireDate = employee.HireDate,
            //     DeptNo = department.DeptNo,
            //     DeptName = department.DeptName,
            //     ManagerEmpNo = manager.EmpNo,
            //     ManagerFirstName = manager.FirstName,
            //     ManagerLastName = manager.LastName,
            //     Salary = salary.Salary,
            //     Title = title.Title
            // };

            IQueryable<VwEmpDetails> query = _context.VwEmpDetails
                .Where(i => i.EmpNo == empNo);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<EmployeeDetail> PutAsync(int empNo, EmployeeDetail employeeDetailUpdateRequest)
        {
            Employees employee = await _context.Employees.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();
            VwSalariesCurrent salary = await _context.VwSalariesCurrent.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();
            VwTitlesCurrent title = await _context.VwTitlesCurrent.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();
            VwDeptEmpCurrent deptEmp = await _context.VwDeptEmpCurrent.Where(i => i.EmpNo == empNo).FirstOrDefaultAsync();

            if (title.Title != employeeDetailUpdateRequest.Title) {
                throw new NotImplementedException();
            }

            if (salary.Salary != employeeDetailUpdateRequest.Salary) {
                throw new NotImplementedException();
            }

            if (deptEmp.DeptNo != employeeDetailUpdateRequest.DeptNo) {
                throw new NotImplementedException();
            }

            if (employeeInfoChanged(employee, employeeDetailUpdateRequest)) {
                employee.BirthDate = employeeDetailUpdateRequest.BirthDate;
                employee.FirstName = employeeDetailUpdateRequest.FirstName;
                employee.LastName = employeeDetailUpdateRequest.LastName;
                employee.Gender = employeeDetailUpdateRequest.Gender;
                employee.HireDate = employeeDetailUpdateRequest.HireDate;

                _context.Entry(employee).State = EntityState.Modified;
            }

            await _context.SaveChangesAsync();
            return employeeDetailUpdateRequest;
        }

        private bool employeeInfoChanged(Employees employee, EmployeeDetail employeeDetailUpdateRequest)
        {
            bool result =
                employee.BirthDate != employeeDetailUpdateRequest.BirthDate ||
                employee.FirstName != employeeDetailUpdateRequest.FirstName ||
                employee.LastName != employeeDetailUpdateRequest.LastName ||
                employee.Gender != employeeDetailUpdateRequest.Gender ||
                employee.HireDate != employeeDetailUpdateRequest.HireDate;

            return result;
        }
    }
}