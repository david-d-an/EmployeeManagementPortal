
using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;
using EMP.Data.Models.Mapped;

namespace EMP.Data.Repos
{
    public interface IDeptManagerCurrentRepository
    {
        Task<IEnumerable<VwDeptManagerCurrent>> GetAsync();
        Task<VwDeptManagerCurrent> GetAsync(string deptNo);
        Task<VwDeptManagerCurrent> PutAsync(string deptNo, VwDeptManagerCurrent deptManagerUpdateRequest);
        Task<VwDeptManagerCurrent> PostAsync(VwDeptManagerCurrent deptManagerCreateRequest);

        // TO DO:
        // DELETE
    }
}
