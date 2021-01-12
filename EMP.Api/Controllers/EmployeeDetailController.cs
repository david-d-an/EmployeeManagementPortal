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

            if (employeeBasicInfoChanged(employee, employeeDetailUpdateRequest)) {
                Employees employeeUpdateRequest = new Employees {
                    EmpNo = id,
                    BirthDate = employeeDetailUpdateRequest.BirthDate,
                    FirstName = employeeDetailUpdateRequest.FirstName,
                    LastName = employeeDetailUpdateRequest .LastName,
                    Gender = employeeDetailUpdateRequest .Gender,
                    HireDate = employeeDetailUpdateRequest.HireDate
                };
                Employees employeeUpdateResult = await _employeeRepository.PutAsync(id.ToString(), employeeUpdateRequest);
            }

            if (deptEmp.DeptNo != employeeDetailUpdateRequest.DeptNo) {
                VwDeptEmpCurrent deptEmpCreateRequest = new VwDeptEmpCurrent {
                    EmpNo = employeeDetailUpdateRequest.EmpNo,
                    DeptNo = employeeDetailUpdateRequest.DeptNo
                };
                VwDeptEmpCurrent deptEmpCreateResult = await _deptEmpRepository.PostAsync(deptEmpCreateRequest);
            }

            if (salary.Salary != employeeDetailUpdateRequest.Salary) {
                VwSalariesCurrent salaryCreateRequest = new VwSalariesCurrent {
                    EmpNo = employeeDetailUpdateRequest.EmpNo,
                    Salary = employeeDetailUpdateRequest.Salary
                };
                VwSalariesCurrent salaryCreateResult = await _salaryRepository.PostAsync(salaryCreateRequest);
            }

            if (title.Title != employeeDetailUpdateRequest.Title) {
                VwTitlesCurrent titleCreateRequest = new VwTitlesCurrent {
                    EmpNo = employeeDetailUpdateRequest.EmpNo,
                    Title = employeeDetailUpdateRequest.Title
                };
                VwTitlesCurrent titleCreateResult = await _titleRepository.PostAsync(titleCreateRequest);
            }

            VwEmpDetails result = await _employeeDetailRepository.GetAsync(id.ToString());

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

            if (empNo != null)
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