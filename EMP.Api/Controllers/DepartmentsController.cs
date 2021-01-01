using System.Collections.Generic;
using EMP.Data.Repos;
using EMP.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IDepartmentsRepository _departmentsRepository;

        public DepartmentsController(
            ILogger<DepartmentsController> logger,
            IDepartmentsRepository departmentsRepository)
        {
            this._logger = logger;
            this._departmentsRepository = departmentsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Departments>> Get()
        {
            return await _departmentsRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<Departments> Get(string id)
        {
            return await _departmentsRepository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<Departments> Put(string id, Departments departmentUpdateRequest)
        {
            Departments result = await _departmentsRepository.PutAsync(id, departmentUpdateRequest);
            return result;
        }

        [HttpPost]
        public async Task<Departments> Post(Departments departmentCreateRequest) 
        {
            Departments result = await _departmentsRepository.PostAsync(departmentCreateRequest);
            return result;
        }
        
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Departments>> Delete(string id) 
        // {
        //     throw new NotImplementedException();
        // }

    }
}