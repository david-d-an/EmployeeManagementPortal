// using System;
// using System.Collections.Generic;
// using EMP.Core.Repos;
// using EMP.Data.Models;

// namespace EMP.Core.Processors
// {
//     public class DepartmentsProcessor
//     {
//         private IDepartmentsRepository _departmentsRepository;

//         public DepartmentsProcessor(IDepartmentsRepository departmentsRepository)
//         {
//             this._departmentsRepository = departmentsRepository;
//         }

//         public IEnumerable<Departments> GetAllDepartments()
//         {
//             IEnumerable<Departments> departments = _departmentsRepository.Get();
//             return departments;
//         }

//         public Departments GetDepartmentByDeptNo(string deptNo)
//         {
//             Departments department = _departmentsRepository.Get(deptNo);
//             return department;
//         }

//         public Departments UpdateDepartmentInfo(Departments departmentUpdateRequest)
//         {
//             Departments department = _departmentsRepository.Put(departmentUpdateRequest);
//             return department;
//         }

//         public Departments CreateDepartment(Departments departmentCreateRequest)
//         {
//             Departments department = _departmentsRepository.Post(departmentCreateRequest);
//             return department;
//         }
//     }
// }