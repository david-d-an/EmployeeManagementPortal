using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
using EMP.Data.Repos;

namespace EMP.DataAccess.Repos
{
    public class SalaryRepository : ISalaryRepository
    {
        public async Task<IEnumerable<VwSalariesCurrent>> GetAsync()
        {
            return await TaskConstants<IEnumerable<VwSalariesCurrent>>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> GetAsync(int empNo)
        {
            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> PostAsync(VwSalariesCurrent salaryCreateRequest)
        {
            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> PutAsync(int empNo, VwSalariesCurrent salaryUpdateRequest)
        {
            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }

        public async Task<VwSalariesCurrent> DeleteAsync(int empNo)
        {
            return await TaskConstants<VwSalariesCurrent>.NotImplemented;
        }
    }
}