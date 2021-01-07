using System.Collections.Generic;
using System.Threading.Tasks;
using EMP.Common.Tasks;
using EMP.Data.Models;
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
        private IRepository<VwTitlesCurrent> _titleRepository;

        public TitleController(
            ILogger<TitleController> logger,
            IRepository<VwTitlesCurrent> titleRepository)
        {
            this._logger = logger;
            this._titleRepository = titleRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return await TaskConstants<ActionResult<IEnumerable<string>>>.NotImplemented;
        }

        [HttpGet("{id}", Name = "Get")]
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