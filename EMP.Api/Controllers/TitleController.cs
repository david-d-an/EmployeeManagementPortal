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
    public class TitleController : ControllerBase
    {
        private ILogger<TitleController> _logger;
        // private IRepository<VwTitlesCurrent> _titleRepository;
        private IRepository<DistinctTitles> _distinctTitleRepository;

        public TitleController(
            ILogger<TitleController> logger,
            // IRepository<VwTitlesCurrent> titleRepository,
            IRepository<DistinctTitles> distinctTitleRepository)
        {
            this._logger = logger;
            // this._titleRepository = titleRepository;
            this._distinctTitleRepository = distinctTitleRepository;
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