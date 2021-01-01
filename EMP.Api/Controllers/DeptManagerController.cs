using System.Collections.Generic;
using EMP.Data.Repos;
using EMP.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using EMP.Common.Tasks;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeptManagerController : ControllerBase
    {
        private ILogger<DeptManagerController> _logger;
        private IDeptManagerRepository _deptManagerRepository;

        public DeptManagerController(
            ILogger<DeptManagerController> logger,
            IDeptManagerRepository deptManagerRepository)
        {
            this._logger = logger;
            this._deptManagerRepository = deptManagerRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<DeptManager>> Get()
        {
            return await _deptManagerRepository.GetAsync();
        }

        [HttpGet("{id}")]
        public async Task<DeptManager> Get(int id)
        {
            return await _deptManagerRepository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<DeptManager> Put(int id, DeptManager deptManagerUpdateRequest)
        {
            DeptManager result = await _deptManagerRepository.PutAsync(id, deptManagerUpdateRequest);
            return result;
        }

        [HttpPost]
        public async Task<DeptManager> Post(DeptManager deptManagerCreateRequest) 
        {
            DeptManager result = await _deptManagerRepository.PostAsync(deptManagerCreateRequest);
            return result;
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<DeptManager>> Delete(int id) 
        {
            // throw new NotImplementedException();
            return await TaskConstants<ActionResult<DeptManager>>.NotImplemented;
        }


    }
}