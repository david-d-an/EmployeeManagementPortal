using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface ISalaryRepository
    {
        Task<IEnumerable<VwSalariesCurrent>> GetAsync();
        Task<VwSalariesCurrent> GetAsync(int empNo);
        Task<VwSalariesCurrent> PutAsync(int empNo, VwSalariesCurrent salaryUpdateRequest);
        Task<VwSalariesCurrent> PostAsync(VwSalariesCurrent salaryCreateRequest);
        Task<VwSalariesCurrent> DeleteAsync(int empNo);

    }
}
