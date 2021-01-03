using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IDeptEmpRepository
    {
        Task<IEnumerable<VwDeptEmpCurrent>> GetAsync();
        Task<VwDeptEmpCurrent> GetAsync(int empNo);
        Task<VwDeptEmpCurrent> PutAsync(int empNo, VwDeptEmpCurrent deptEmpUpdateRequest);
        Task<VwDeptEmpCurrent> PostAsync(VwDeptEmpCurrent deptEmpCreateRequest);
        Task<VwDeptEmpCurrent> DeleteAsync(int empNo);
    }
}