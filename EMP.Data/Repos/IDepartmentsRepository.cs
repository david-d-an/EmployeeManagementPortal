using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;

namespace EMP.Data.Repos
{
    public interface IDepartmentsRepository
    {
        // IEnumerable<Departments> Get();
        // Departments Get(string deptNo);
        // Departments Put(string empNo, Departments departmentUpdateRequest);
        // Departments Post(Departments departmentCreateRequest);

        Task<IEnumerable<Departments>> GetAsync();
        Task<Departments> GetAsync(string deptNo);
        Task<Departments> PutAsync(string deptNo, Departments departmentUpdateRequest);
        Task<Departments> PostAsync(Departments departmentCreateRequest);

        // TO DO:
        // DELETE

    }
}