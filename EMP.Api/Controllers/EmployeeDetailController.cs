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
        private ILogger<DepartmentsController> _logger;
        private IEmployeeDetailRepository _employeeDetailRepository;

        public EmployeeDetailController(
            ILogger<DepartmentsController> logger,
            IEmployeeDetailRepository employeeDetailRepository)
        {
            this._logger = logger;
            this._employeeDetailRepository = employeeDetailRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<VwEmpDetails>> Get()
        {
            // IEnumerable<VwEmpDetails> r = await _employeeDetailRepository.GetAsync();
            // List<VwEmpDetails> l = r.ToList();
            // long c = l.Count();

            return await _employeeDetailRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<VwEmpDetails> Get(int id)
        {
            return await _employeeDetailRepository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<EmployeeDetail> Put(int id, EmployeeDetail employeeDetailUpdateRequest)
        {
            EmployeeDetail result = await _employeeDetailRepository
                                          .PutAsync(id, employeeDetailUpdateRequest);
            return result;
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDetail>> Post(EmployeeDetail employeeDetail) 
        {
            // throw new NotImplementedException();
            return await TaskConstants<ActionResult<EmployeeDetail>>.NotImplemented;            
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<EmployeeDetail>> Delete(long id) 
        {
            // throw new NotImplementedException();
            return await TaskConstants<ActionResult<EmployeeDetail>>.NotImplemented;
        }
    }
}