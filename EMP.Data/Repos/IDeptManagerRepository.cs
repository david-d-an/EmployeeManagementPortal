using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IDeptManagerRepository
    {
        Task<IEnumerable<DeptManager>> GetAsync();
        Task<DeptManager> GetAsync(int id);
        Task<DeptManager> PutAsync(int id, DeptManager deptManagerUpdateRequest);
        Task<DeptManager> PostAsync(DeptManager deptManagerCreateRequest);

        // TO DO:
        // DELETE
    }
}