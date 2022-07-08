using System.Collections.Generic;
using EMP.Data.Repos;
using EMP.Data.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EMP.Api.Controllers
{
    [ApiController]
    [Authorize(Roles="System Admin,Department Manager")]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly ILogger<DepartmentsController> _logger;
        private readonly IRepository<Departments> _departmentsRepository;
        private readonly IUnitOfWorkEmployees _unitOfWork;


        // public async ValueTask<int> foo(int param) {
        //     return await
        //         new ValueTask<int>(param * 2);
        //         Func<int, bool> action;
        //         Dictionary<int, string> d = new Dictionary<int, string> {
        //             {1, "One"},
        //             {2, "Two"},
        //             {3, "Three"},
        //         };
        //         IEnumerable<int> keys = d.Keys;
        //         IEnumerable<string> values = d.Values;
        // }

        public DepartmentsController(
            ILogger<DepartmentsController> logger,
            IRepository<Departments> departmentsRepository,
            IUnitOfWorkEmployees unitOfWork)
        {
            this._logger = logger;
            this._departmentsRepository = departmentsRepository;
            this._unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Departments>>> Get()
        {
            _logger.LogInformation("Getting all departments");
            await Task.Delay(0);
            return Ok(_departmentsRepository.GetAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Departments>> Get(string id)
        {
            return await _departmentsRepository.GetAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Departments>> Put(string id, Departments departmentUpdateRequest)
        {
            // Departments department = await _departmentsRepository.GetAsync(id);
            Departments department = await _unitOfWork.DepartmentsRepository.GetAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            // Departments result = await _departmentsRepository.PutAsync(id, departmentUpdateRequest);
            Departments result = await _unitOfWork.DepartmentsRepository.PutAsync(id, departmentUpdateRequest);
            _unitOfWork.Commit();

            return result;
        }

        [HttpPost]
        public async Task<ActionResult<Departments>> Post(Departments departmentCreateRequest) 
        {
            Departments result = await _departmentsRepository.PostAsync(departmentCreateRequest);
            var actionName = nameof(Post);
            var controllerName = nameof(DepartmentsController);
            return CreatedAtAction(
                actionName,
                controllerName, 
                new { id = result.DeptNo }, 
                result);
       }
        
        // [HttpDelete("{id}")]
        // public async Task<ActionResult<Departments>> Delete(string id) 
        // {
        //     throw new NotImplementedException();
        // }

    }
}