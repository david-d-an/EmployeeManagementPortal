using System.Collections.Generic;
using EMP.Data.Models;
using EMP.Core.Repos;
using EMP.DataDataAccess.Context;
using System.Linq;

namespace EMP.DataAccess.Repos
{
    public class DepartmentsRepository : IDepartmentsRepository
    {
        private EmployeesContext _context;

        public DepartmentsRepository(EmployeesContext context)
        {
            this._context = context;
        }

        Departments IDepartmentsRepository.Get(string deptNo)
        {
            throw new System.NotImplementedException();
        }

        IEnumerable<Departments> IDepartmentsRepository.Get()
        {
            return this._context.Departments.ToList();
        }

        Departments IDepartmentsRepository.Put(Departments departmentUpdateRequest)
        {
            throw new System.NotImplementedException();
        }

        Departments IDepartmentsRepository.Post(Departments departmentCreateRequest)
        {
            throw new System.NotImplementedException();
        }
    }
}