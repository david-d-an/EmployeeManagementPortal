using System;
using System.Collections.Generic;
using System.Linq;
using EMP.Core.Repos;
using EMP.Data.Models;

namespace EMP.Core.Processors
{
    public class EmployeeProcessor
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeProcessor(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        public Employees GetEmployeeByEmpNo(int empNo)
        {
            Employees e = _employeeRepository.Get(empNo);
            return e;
        }

        public Employees UpdateEmployeeInfo(Employees employeeUpdateRequest)
        {
            Employees e = _employeeRepository.Put(employeeUpdateRequest);
            return e;
        }

        public Employees CreateEmployee(EmployeeRequest employeeCreateRequest)
        {
            Employees e = _employeeRepository.Post(employeeCreateRequest);
            return e;
        }

        public Employees DeleteEmployee(int empNo)
        {
            Employees e = _employeeRepository.Delete(empNo);
            return e;
        }

        public IEnumerable<Employees> GetAllEmployees()
        {
            IEnumerable<Employees> employees = _employeeRepository.Get();
            return employees;
        }
    }
}