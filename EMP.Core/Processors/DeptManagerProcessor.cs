using System;
using System.Collections.Generic;
using EMP.Core.Repos;
using EMP.Data.Models;

namespace EMP.Core.Processors
{
    public class DeptManagerProcessor
    {
        private IDeptManagerRepository _deptManagerRepository;

        public DeptManagerProcessor(IDeptManagerRepository deptManagerRepository)
        {
            this._deptManagerRepository = deptManagerRepository;
        }

        public DeptManager GetDeptManagerByEmpNo(int empNo)
        {
            DeptManager deptManager = _deptManagerRepository.Get(empNo);
            return deptManager;
        }

        public IEnumerable<DeptManager> GetAllDeptManagers()
        {
            IEnumerable<DeptManager> deptManagers = _deptManagerRepository.Get();
            return deptManagers;
        }
    }
}