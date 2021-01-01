using System.Collections.Generic;
using EMP.Data.Models;
using EMP.Data.Repos;
using EMP.DataDataAccess.Context;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System;
using EMP.Common.Tasks;

namespace EMP.DataAccess.Repos
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private EmployeesContext _context;

        public DepartmentsRepository(EmployeesContext context)
        {
            this._context = context;
        }

        // public Departments Get(string deptNo)
        // {
        //     throw new System.NotImplementedException();
        // }

        public async Task<Departments> GetAsync(string deptNo)
        {
            IQueryable<Departments> result = 
                from d in this._context.Departments
                where d.DeptNo == deptNo
                select d;

            return await result.FirstOrDefaultAsync();
        }

        // public IEnumerable<Departments> Get()
        // {
        //     IQueryable<Departments> result = 
        //         from d in this._context.Departments
        //         // join de in this._context.DeptEmp
        //         // on d.DeptNo equals de.DeptNo
        //         // join e in this._context.Employees
        //         // on de.EmpNo equals e.EmpNo
        //         select d;

        //     return result.ToList();
        // }


        public async Task<IEnumerable<Departments>> GetAsync()
        {
            IQueryable<Departments> result = 
                from d in this._context.Departments
                select d;

            return await result.ToListAsync();
        }

        // public Departments Put(string deptNo, Departments departmentUpdateRequest)
        // {
        //     throw new System.NotImplementedException();
        // }
        public async Task<Departments> PutAsync(string deptNo, Departments departmentUpdateRequest)
        {
            IQueryable<Departments> result = 
                from d in this._context.Departments
                where d.DeptNo == deptNo
                select d;

            var dept = await result.FirstAsync();
            dept.DeptName = departmentUpdateRequest.DeptName;

            _context.Entry(dept).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return dept;
        }

        // public Departments Post(Departments departmentCreateRequest)
        // {
        //     throw new System.NotImplementedException();
        // }
        public async Task<Departments> PostAsync(Departments departmentCreateRequest)
        {
            // throw new NotImplementedException();
            return await TaskConstants<Departments>.NotImplemented;
        }

    }
}