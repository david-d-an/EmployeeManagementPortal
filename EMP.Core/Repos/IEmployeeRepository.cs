using System.Collections.Generic;
using EMP.Data.Models;

namespace EMP.Core.Repos
{
    public interface IEmployeeRepository
    {
        // IEnumerable<Employees> Get(Employees searchRequest);
        IEnumerable<Employees> Get();
        Employees Get(int empNo);
        Employees Put(Employees employee);
        Employees Post(EmployeeRequest employeeUpdateRequest);
        Employees Delete(int empNo);
    }
}