using System.Collections.Generic;
using EMP.Core.Repos;
using EMP.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public IEnumerable<Departments> Get()
        {
            return _departmentsRepository.Get();
        }
    }
}