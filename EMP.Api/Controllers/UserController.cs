using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models.Employees;
using EMP.Data.Models.Sts;
using EMP.Data.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private ILogger<UserController> _logger;
        private IRepository<Aspnetusers> _aspnetusersRepository;

        public UserController(
            ILogger<UserController> logger,
            IRepository<Aspnetusers> aspnetusersRepository)
        {
            this._logger = logger;
            this._aspnetusersRepository = aspnetusersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aspnetusers>>> Get()
        {
            await Task.Delay(0);
            return Ok(_aspnetusersRepository.GetAsync());            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aspnetusers>> Get(string id)
        {
            return await _aspnetusersRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(string titleCreateRequest) 
        {
            return await TaskConstants<ActionResult<string>>.NotImplemented;           
        }
    }
}