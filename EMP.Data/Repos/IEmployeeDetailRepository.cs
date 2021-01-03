using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IEmployeeDetailRepository
    {
        Task<VwEmpDetails> GetAsync(int empNo);
        Task<IEnumerable<VwEmpDetails>> GetAsync();
        Task<VwEmpDetails> PutAsync(int id, VwEmpDetails employeeDetailUpdateRequest);
        Task<VwEmpDetails> PostAsync(VwEmpDetails employeeDetailCreateRequest);
    }
}