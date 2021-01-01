using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IEmployeeRepository
    {
        // IEnumerable<Employees> Get(Employees searchRequest);
        // IEnumerable<Employees> Get();
        // Employees Get(int empNo);
        // Employees Put(Employees employee);
        // Employees Post(EmployeeRequest employeeUpdateRequest);
        // Employees Delete(int empNo);
        Task<IEnumerable<Employees>> GetAsync();
        Task<Employees> GetAsync(int id);
        Task<Employees> PutAsync(int id, Employees employeeUpdateRequest);
        Task<Employees> PostAsync(Employees employeeCreateRequest);
        Task<Employees> DeleteAsync(int empNo);
    }
}