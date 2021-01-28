using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Data.Repos;
using EMP.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private ILogger<EmployeeController> _logger;
        private IRepository<Employees> _employeeRepository;

        public EmployeeController(
            ILogger<EmployeeController> logger,
            IRepository<Employees> employeeRepository)
        {
            this._logger = logger;
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employees>>> Get()
        {
            await Task.Delay(0);
            IEnumerable<Employees> result = _employeeRepository.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employees>> Get(int id)
        {
            return await _employeeRepository.GetAsync(id.ToString());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Employees>> Put(int id, Employees employeeUpdateRequest)
        {
            Employees employee = await _employeeRepository.GetAsync(id.ToString());
            if (employee == null)
            {
                return NotFound();
            }

            Employees result = await _employeeRepository.PutAsync(id.ToString(), employeeUpdateRequest);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Employees>> Post(Employees employeeCreateRequest) 
        {
            Employees result = await _employeeRepository.PostAsync(employeeCreateRequest);
            return CreatedAtAction(
                nameof(Post), 
                nameof(EmployeeController), 
                new { id = result.EmpNo }, 
                result);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Employees>> Delete(int id) 
        {
            Employees employee = await _employeeRepository.GetAsync(id.ToString());
            if (employee == null)
            {
                return NotFound();
            }

            Employees deletedEmployee = await _employeeRepository.DeleteAsync(id.ToString());
            return deletedEmployee;
        }

        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Employees>>> GetLarge()
        // {
        //     await Task.Delay(0);
        //     IEnumerable<Employees> result = _employeeRepository.GetAsync();
        //     return Ok(result);
        // }
    }
}

