// using System;
// using System.Collections.Generic;
// using System.Linq;
// using EMP.Core.Repos;
// using EMP.Data.Models;

// namespace EMP.Core.Processors
// {
//     public class EmployeeDetailProcessor
//     {
//         private IEmployeeDetailRepository _employeeDetailRepository;

//         public EmployeeDetailProcessor(IEmployeeDetailRepository employeeDetailRepository)
//         {
//             this._employeeDetailRepository = employeeDetailRepository;
//         }

//         public EmployeeDetail GetEmployeeDetailByEmpNo(int empNo)
//         {
//             var result = _employeeDetailRepository.Get(empNo);
//             return result;
//         }
//     }
// }