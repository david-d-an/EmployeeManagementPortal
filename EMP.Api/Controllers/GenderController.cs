using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models.Employees;
using EMP.Data.Repos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenderController : ControllerBase
    {
        private ILogger<GenderController> _logger;
        private IRepository<DistinctGenders> _distinctTitleRepository;

        public GenderController(
            ILogger<GenderController> logger,
            IRepository<DistinctGenders> distinctGendersRepository)
        {
            this._logger = logger;
            this._distinctTitleRepository = distinctGendersRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            await Task.Delay(0);
            return Ok(_distinctTitleRepository.GetAsync());            
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(string id)
        {
            return await TaskConstants<ActionResult<string>>.NotImplemented;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Post(string titleCreateRequest) 
        {
            return await TaskConstants<ActionResult<string>>.NotImplemented;           
        }
    }
}