using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface ITitleRepository
    {
        Task<IEnumerable<VwTitlesCurrent>> GetAsync();
        Task<VwTitlesCurrent> GetAsync(string id);
        Task<VwTitlesCurrent> PutAsync(string id, VwTitlesCurrent updateRequest);
        Task<VwTitlesCurrent> PostAsync(VwTitlesCurrent createRequest);
        Task<VwTitlesCurrent> DeleteAsync(string id);
    }
}