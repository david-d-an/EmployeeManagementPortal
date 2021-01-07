using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employees>> GetAsync();
        Task<Employees> GetAsync(string id);
        Task<Employees> PutAsync(string id, Employees updateRequest);
        Task<Employees> PostAsync(Employees createRequest);
        Task<Employees> DeleteAsync(string id);
    }
}