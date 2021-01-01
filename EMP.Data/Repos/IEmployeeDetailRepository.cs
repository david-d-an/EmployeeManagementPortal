using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace EMP.Data.Repos
{
    public interface IEmployeeDetailRepository
    {
        Task<VwEmpDetails> GetAsync(int empNo);
        Task<IEnumerable<VwEmpDetails>> GetAsync();
        Task<EmployeeDetail> PutAsync(int id, EmployeeDetail employeeDetailUpdateRequest);
    }
}