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
        private IEmployeeDetailRepository _employeeDetailRepository;
        private IEmployeeRepository _employeeRepository;
        private IDeptEmpRepository _deptEmpRepository;
        private ISalaryRepository _salaryRepository;
        private ITitleRepository _titleRepository;

        public EmployeeDetailController(
            ILogger<EmployeeDetailController> logger,
            IEmployeeRepository employeeRepository,
            IEmployeeDetailRepository employeeDetailRepository,
            IDeptEmpRepository deptEmpRepository,
            ISalaryRepository salaryRepository,
            ITitleRepository titleRepository)
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
            // IEnumerable<VwEmpDetails> r = await _employeeDetailRepository.GetAsync();
            // List<VwEmpDetails> l = r.ToList();
            // long c = l.Count();

            return Ok(await _employeeDetailRepository.GetAsync());
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<VwEmpDetails>> Get(int id)
        {
            return await _employeeDetailRepository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VwEmpDetails>> Put(int id, VwEmpDetails employeeDetailUpdateRequest)
        {
            Employees employee = await _employeeRepository.GetAsync(id);
            VwSalariesCurrent salary = await _salaryRepository.GetAsync(id);
            VwTitlesCurrent title = await _titleRepository.GetAsync(id);
            VwDeptEmpCurrent deptEmp = await _deptEmpRepository.GetAsync(id);

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

            VwEmpDetails result = await _employeeDetailRepository.GetAsync(id);

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
        public async Task<ActionResult<EmployeeDetail>> Delete(long id) 
        {
            return await TaskConstants<ActionResult<EmployeeDetail>>.NotImplemented;
        }
    }
}