using System.Collections.Generic;
using System.Threading.Tasks;

namespace EMP.Data.Repos
{
    // TO DO: need to make it enclosed inside Unit of Work
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAsync(
            object parameters = null, 
            int? pageNum = null, int? 
            pageSize = null);
        Task<T> GetAsync(string id);
        Task<T> PutAsync(string id, T updateRequest);
        Task<T> PostAsync(T createRequest);
        Task<T> DeleteAsync(string id);
    }

    // public interface IRepository2<T> where T : class
    // {
    //     IEnumerable<T> GetAsync(int? pageNum = null, int? pageSize = null);
    //     Task<T> GetAsync(string id);
    // }
}