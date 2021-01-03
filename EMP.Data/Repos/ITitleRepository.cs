using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface ITitleRepository
    {
        Task<IEnumerable<VwTitlesCurrent>> GetAsync();
        Task<VwTitlesCurrent> GetAsync(int empNo);
        Task<VwTitlesCurrent> PutAsync(int empNo, VwTitlesCurrent titleUpdateRequest);
        Task<VwTitlesCurrent> PostAsync(VwTitlesCurrent titleCreateRequest);
        Task<VwTitlesCurrent> DeleteAsync(int empNo);
    }
}