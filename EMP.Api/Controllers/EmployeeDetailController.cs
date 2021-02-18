using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Repos;
using EMP.Data.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using EMP.Common.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeDetailController : ControllerBase
    {
        private ILogger<EmployeeDetailController> _logger;
        private IRepository<Employees> _employeeRepository;
        private IRepository<VwEmpDetails> _employeeDetailRepository;
        private IRepository<VwEmpDetailsShort> _employeeDetailShortRepository;
        private IRepository<VwDeptEmpCurrent> _deptEmpRepository;
        private IRepository<VwSalariesCurrent> _salaryRepository;
        private IRepository<VwTitlesCurrent> _titleRepository;

        public EmployeeDetailController(
            ILogger<EmployeeDetailController> logger,
            IRepository<Employees> employeeRepository,
            IRepository<VwEmpDetails> employeeDetailRepository,
            IRepository<VwEmpDetailsShort> employeeDetailShortRepository,
            IRepository<VwDeptEmpCurrent> deptEmpRepository,
            IRepository<VwSalariesCurrent> salaryRepository,
            IRepository<VwTitlesCurrent> titleRepository)
        {
            this._logger = logger;
            this._employeeRepository = employeeRepository;
            this._employeeDetailRepository = employeeDetailRepository;
            this._employeeDetailShortRepository = employeeDetailShortRepository;
            this._deptEmpRepository = deptEmpRepository;
            this._salaryRepository = salaryRepository;
            this._titleRepository = titleRepository;
        }

        [HttpGet]
        [Authorize(Roles="System Admin")]
        public async Task<ActionResult<IEnumerable<VwEmpDetailsShort>>> Get(
            [FromQuery] int? pageNum,
            [FromQuery] int? pageSize,
            [FromQuery] string firstName,
            [FromQuery] string lastName,
            [FromQuery] string salaryMin,
            [FromQuery] string salaryMax,
            [FromQuery] string title,
            [FromQuery] string deptName)
        {
            // api/EmployeeDetail?firstName=john&lastName=smith&salaryMin=4&salaryMax=100&title=admin&deptName=marketing

            object parameters = new List<KeyValuePair<string, string>> {
                new KeyValuePair<string, string> ("firstName", firstName ),
                new KeyValuePair<string, string> ("lastName", lastName ),
                new KeyValuePair<string, string> ("salaryMin", salaryMin ),
                new KeyValuePair<string, string> ("salaryMax", salaryMax ),
                new KeyValuePair<string, string> ("title", title ),
                new KeyValuePair<string, string> ("deptName", deptName ),
            };

            await Task.Delay(0);
            return Ok(_employeeDetailShortRepository.GetAsync(parameters, pageNum, pageSize));
        }

        [HttpGet("{id}")]
        [Authorize(Roles="System Admin")]
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

            if (employee == null) {
                return NotFound();
            }

            if (employeeBasicInfoChanged(employee, employeeDetailUpdateRequest)) {
                Employees employeeUpdateRequest = new Employees {
                    EmpNo = id,
                    BirthDate = employeeDetailUpdateRequest.BirthDate,
                    FirstName = employeeDetailUpdateRequest.FirstName,
                    LastName = employeeDetailUpdateRequest .LastName,
                    Gender = employeeDetailUpdateRequest .Gender,
                    HireDate = employeeDetailUpdateRequest.HireDate
                };
                Employees employeeUpdateResult = await 
                    _employeeRepository
                    .PutAsync(id.ToString(), employeeUpdateRequest);
            }

            if (deptEmp?.DeptNo != employeeDetailUpdateRequest.DeptNo) {
                VwDeptEmpCurrent deptEmpCreateRequest = new VwDeptEmpCurrent {
                    EmpNo = employeeDetailUpdateRequest.EmpNo,
                    DeptNo = employeeDetailUpdateRequest.DeptNo
                };
                VwDeptEmpCurrent deptEmpCreateResult = await 
                    _deptEmpRepository
                    .PostAsync(deptEmpCreateRequest);
            }

            if (salary?.Salary != employeeDetailUpdateRequest.Salary) {
                VwSalariesCurrent salaryCreateRequest = new VwSalariesCurrent {
                    EmpNo = employeeDetailUpdateRequest.EmpNo,
                    Salary = employeeDetailUpdateRequest.Salary
                };
                VwSalariesCurrent salaryCreateResult = await 
                    _salaryRepository
                    .PostAsync(salaryCreateRequest);
            }

            if (title?.Title != employeeDetailUpdateRequest.Title) {
                VwTitlesCurrent titleCreateRequest = new VwTitlesCurrent {
                    EmpNo = employeeDetailUpdateRequest.EmpNo,
                    Title = employeeDetailUpdateRequest.Title
                };
                VwTitlesCurrent titleCreateResult = await 
                    _titleRepository
                    .PostAsync(titleCreateRequest);
            }

            VwEmpDetails result = await 
                _employeeDetailRepository
                .GetAsync(id.ToString());

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<VwEmpDetails>> Post(VwEmpDetails employeeDetailCreateRequest) 
        {
            Employees employeeCreateRequest = new Employees {
                EmpNo = -1,
                BirthDate = employeeDetailCreateRequest.BirthDate,
                FirstName = employeeDetailCreateRequest.FirstName,
                LastName = employeeDetailCreateRequest .LastName,
                Gender = employeeDetailCreateRequest .Gender,
                HireDate = employeeDetailCreateRequest.HireDate
            };

            Employees employeeCreateResult = await _employeeRepository.PostAsync(employeeCreateRequest);
            int? empNo = employeeCreateResult?.EmpNo;

            if (empNo == null)
                return BadRequest();

            VwDeptEmpCurrent deptEmpCreateRequest = new VwDeptEmpCurrent {
                EmpNo = empNo.Value,
                DeptNo = employeeDetailCreateRequest.DeptNo
            };
            VwDeptEmpCurrent deptEmpCreateResult = await _deptEmpRepository.PostAsync(deptEmpCreateRequest);

            VwSalariesCurrent salaryCreateRequest = new VwSalariesCurrent {
                EmpNo = empNo.Value,
                Salary = employeeDetailCreateRequest.Salary
            };
            VwSalariesCurrent salaryCreateResult = await _salaryRepository.PostAsync(salaryCreateRequest);

            VwTitlesCurrent titleCreateRequest = new VwTitlesCurrent {
                EmpNo = empNo.Value,
                Title = employeeDetailCreateRequest.Title
            };
            VwTitlesCurrent titleCreateResult = await _titleRepository.PostAsync(titleCreateRequest);

            VwEmpDetails result = await _employeeDetailRepository.GetAsync(empNo.Value.ToString());

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

        private bool employeeBasicInfoChanged(Employees employee, VwEmpDetails employeeDetailUpdateRequest)
        {
            return    
                employee.BirthDate != employeeDetailUpdateRequest.BirthDate ||
                employee.FirstName != employeeDetailUpdateRequest.FirstName ||
                employee.LastName != employeeDetailUpdateRequest.LastName ||
                employee.Gender != employeeDetailUpdateRequest.Gender ||
                employee.HireDate != employeeDetailUpdateRequest.HireDate;
        }
    }
}