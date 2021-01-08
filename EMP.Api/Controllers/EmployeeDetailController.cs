using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Repos;
using EMP.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using EMP.Common.Tasks;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDetailController : ControllerBase
    {
        private ILogger<EmployeeDetailController> _logger;
        private IRepository<VwEmpDetails> _employeeDetailRepository;
        private IRepository<Employees> _employeeRepository;
        private IRepository<VwDeptEmpCurrent> _deptEmpRepository;
        private IRepository<VwSalariesCurrent> _salaryRepository;
        private IRepository<VwTitlesCurrent> _titleRepository;

        public EmployeeDetailController(
            ILogger<EmployeeDetailController> logger,
            IRepository<Employees> employeeRepository,
            IRepository<VwEmpDetails> employeeDetailRepository,
            IRepository<VwDeptEmpCurrent> deptEmpRepository,
            IRepository<VwSalariesCurrent> salaryRepository,
            IRepository<VwTitlesCurrent> titleRepository)
        {
            this._logger = logger;
            this._employeeDetailRepository = employeeDetailRepository;
            this._employeeRepository = employeeRepository;
            this._deptEmpRepository = deptEmpRepository;
            this._salaryRepository = salaryRepository;
            this._titleRepository = titleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwEmpDetails>>> Get()
        {
            return Ok(await _employeeDetailRepository.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VwEmpDetails>> Get(int id)
        {
            return await _employeeDetailRepository.GetAsync(id.ToString());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VwEmpDetails>> Put(int id, VwEmpDetails employeeDetailUpdateRequest)
        {
            Employees employee = await _employeeRepository.GetAsync(id.ToString());
            VwSalariesCurrent salary = await _salaryRepository.GetAsync(id.ToString());
            VwTitlesCurrent title = await _titleRepository.GetAsync(id.ToString());
            VwDeptEmpCurrent deptEmp = await _deptEmpRepository.GetAsync(id.ToString());

            if (employee == null)
            {
                return NotFound();
            }

            if (title.Title != employeeDetailUpdateRequest.Title) {
                throw new NotImplementedException();
            }

            if (salary.Salary != employeeDetailUpdateRequest.Salary) {
                throw new NotImplementedException();
            }

            if (deptEmp.DeptNo != employeeDetailUpdateRequest.DeptNo) {
                throw new NotImplementedException();
            }

            if (employeeBasicInfoChanged(employee, employeeDetailUpdateRequest)) {
                throw new NotImplementedException();
            }

            VwEmpDetails result = await _employeeDetailRepository.GetAsync(id.ToString());

            return result;
        }

        private bool employeeBasicInfoChanged(Employees employee, VwEmpDetails employeeDetailUpdateRequest)
        {
            return    
                employee.BirthDate != employeeDetailUpdateRequest.BirthDate ||
                employee.FirstName != employeeDetailUpdateRequest.FirstName ||
                employee.LastName != employeeDetailUpdateRequest.LastName ||
                employee.Gender != employeeDetailUpdateRequest.Gender ||
                employee.HireDate != employeeDetailUpdateRequest.HireDate;
        }

        [HttpPost]
        public async Task<ActionResult<VwEmpDetails>> Post(VwEmpDetails employeeDetailCreateRequest) 
        {
            // return await TaskConstants<ActionResult<EmployeeDetail>>.NotImplemented; 
            VwEmpDetails result = await _employeeDetailRepository.PostAsync(employeeDetailCreateRequest);
            return CreatedAtAction(
                nameof(Post), 
                nameof(EmployeeDetailController), 
                new { id = result.EmpNo }, 
                result);
           
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<VwEmpDetails>> Delete(long id) 
        {
            return await TaskConstants<ActionResult<VwEmpDetails>>.NotImplemented;
        }
    }
}