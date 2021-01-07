using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IDeptEmpRepository
    {
        Task<IEnumerable<VwDeptEmpCurrent>> GetAsync();
        Task<VwDeptEmpCurrent> GetAsync(string id);
        Task<VwDeptEmpCurrent> PutAsync(string id, VwDeptEmpCurrent updateRequest);
        Task<VwDeptEmpCurrent> PostAsync(VwDeptEmpCurrent createRequest);
        Task<VwDeptEmpCurrent> DeleteAsync(string id);
    }
}