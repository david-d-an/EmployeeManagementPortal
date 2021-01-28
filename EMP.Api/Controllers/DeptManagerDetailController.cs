using System.Collections.Generic;
using EMP.Data.Repos;
using EMP.Data.Models;
using EMP.Data.Models.Mapped;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using System.Linq;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeptManagerDetailController : ControllerBase
    {
        private ILogger<DeptManagerDetailController> _logger;
        private IRepository<VwDeptManagerCurrent> _deptManagerCurrentRepository;
        private IRepository<VwDeptManagerDetail> _deptManagerDetailRepository;
        private IRepository<Employees> _employeeRepository;
        private IRepository<VwDeptEmpCurrent> _deptEmpRepository;
        private IRepository<VwTitlesCurrent> _titleRepository;
        private IRepository<Departments> _departmentsRepository;

        public DeptManagerDetailController(
            ILogger<DeptManagerDetailController> logger,
            IRepository<Employees> employeeRepository,
            IRepository<VwDeptEmpCurrent> deptEmpRepository,
            IRepository<VwTitlesCurrent> titleRepository,
            IRepository<VwDeptManagerCurrent> deptManagerCurrentRepository,
            IRepository<VwDeptManagerDetail> deptManagerDetailRepository,
            IRepository<Departments> departmentsRepository)
        {
            this._logger = logger;
            this._employeeRepository = employeeRepository;
            this._deptEmpRepository = deptEmpRepository;
            this._titleRepository = titleRepository;
            this._deptManagerCurrentRepository = deptManagerCurrentRepository;
            this._deptManagerDetailRepository = deptManagerDetailRepository;
            this._departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VwDeptManagerDetail>>> Get()
        {
            await Task.Delay(0);
            return Ok(_deptManagerDetailRepository.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VwDeptManagerDetail>> Get(string id)
        {
            return await _deptManagerDetailRepository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentManagerDetail>> Put(string id, DepartmentManagerDetail deptManagerUpdateRequest)
        {
            Departments departmentCheck = await _departmentsRepository.GetAsync(deptManagerUpdateRequest.DeptNo);
            if (departmentCheck == null) {
                return NotFound();
            }
            Employees employeeCheck = await _employeeRepository.GetAsync(deptManagerUpdateRequest.EmpNo.Value.ToString());
            if (employeeCheck == null) {
                return NotFound();
            }

            VwDeptManagerCurrent vwDeptManagerCurrent = 
                new VwDeptManagerCurrent {
                    EmpNo = deptManagerUpdateRequest.EmpNo.Value,
                    DeptNo = deptManagerUpdateRequest.DeptNo,
                    FromDate = deptManagerUpdateRequest.FromDate.Value,
                    ToDate = deptManagerUpdateRequest.ToDate.Value,
                };
            VwDeptManagerCurrent result = await _deptManagerCurrentRepository.PutAsync(id, vwDeptManagerCurrent);
            Employees employee = await _employeeRepository.GetAsync(result.EmpNo.ToString());
            Departments department = await _departmentsRepository.GetAsync(result.DeptNo);

            DepartmentManagerDetail updatedDepartmentManagerDetail =    
                new DepartmentManagerDetail {
                    DeptNo = result.DeptNo,
                    DeptName = department.DeptName,
                    FromDate = result.FromDate,
                    ToDate = result.ToDate,
                    EmpNo = employee.EmpNo,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                };
            return updatedDepartmentManagerDetail;
        }

        [HttpPost]
        public async Task<ActionResult<DepartmentManagerDetail>> Post(DepartmentManagerDetail deptManagerCreateRequest) 
        {

            VwDeptManagerCurrent vwDeptManagerCurrent = 
                new VwDeptManagerCurrent {
                    EmpNo = deptManagerCreateRequest.EmpNo.Value,
                    DeptNo = deptManagerCreateRequest.DeptNo,
                    FromDate = deptManagerCreateRequest.FromDate.Value,
                    ToDate = deptManagerCreateRequest.ToDate.Value,
                };
            ActionResult<VwDeptManagerCurrent> postResult = await _deptManagerCurrentRepository.PostAsync(vwDeptManagerCurrent);
            VwDeptManagerCurrent value = postResult.Value;
            Employees employee = await _employeeRepository.GetAsync(value.EmpNo.ToString());
            Departments department = await _departmentsRepository.GetAsync(value.DeptNo);

            var result = new DepartmentManagerDetail {
                DeptNo = value.DeptNo,
                DeptName = department.DeptName,
                FromDate = value.FromDate,
                ToDate = value.ToDate,
                EmpNo = value.EmpNo,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
            };

            return CreatedAtAction(
                nameof(Post), 
                nameof(DeptManagerDetailController), 
                new { EmpNo = result.EmpNo, DeptNo = result.DeptNo }, 
                result);
        }
        
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<DeptManager>> Delete(int id) 
        // {
        //     // throw new NotImplementedException();
        //     return await TaskConstants<ActionResult<DeptManager>>.NotImplemented;
        // }
    }
}