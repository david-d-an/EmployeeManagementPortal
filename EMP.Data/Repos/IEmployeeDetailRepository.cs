using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IEmployeeDetailRepository
    {
        Task<VwEmpDetails> GetAsync(string id);
        Task<IEnumerable<VwEmpDetails>> GetAsync();
        Task<VwEmpDetails> PutAsync(string id, VwEmpDetails employeeDetailUpdateRequest);
        Task<VwEmpDetails> PostAsync(VwEmpDetails employeeDetailCreateRequest);
        Task<VwEmpDetails> DeleteAsync(string id);
    }
}