using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IDeptManagerRepository
    {
        Task<IEnumerable<DeptManager>> GetAsync();
        Task<DeptManager> GetAsync(string deptNo);
        Task<DeptManager> PutAsync(string deptNo, DeptManager deptManagerUpdateRequest);
        Task<DeptManager> PostAsync(DeptManager deptManagerCreateRequest);

        // TO DO:
        // DELETE
    }
}