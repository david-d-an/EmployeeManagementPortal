using System.Collections.Generic;
using EMP.Data.Repos;
using EMP.Data.Models.Employees;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading;

namespace EMP.Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
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

        public TestController(
            ILogger<DepartmentsController> logger,
            IRepository<Departments> departmentsRepository,
            IUnitOfWorkEmployees unitOfWork)
        {
            this._logger = logger;
            this._departmentsRepository = departmentsRepository;
            this._unitOfWork = unitOfWork;
        }

        // [Flags]
        public enum Features {
            None = 0,
            Brakes = 1,
            Radio = 2,
            AirConditioning = 4
        }

        
        // air | Brake == Air
        // 101 | 001

        private void Write(int val) {
            Console.WriteLine(
                $"Convert.ToSingle(val,2).PadLeft(8,'0')"
            );
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Departments>>> Get()
        {

            var f = Features.AirConditioning | Features.Brakes;
            

             


            // List<Task> taskList = new List<Task>();
            // for (int i = 0; i <= 5; i++)
            // {
            //     var i1 = i;
            //     // taskList.Add(Task.Run(async () => await this.foo(i1)));
            //     // taskList.Add(this.foo(i));
            //     // taskList.Add(Task.Run(() => this.bar(i1)));
            //     taskList.Add(this.bar(i));
            // }
            // // Task.WaitAll(taskList.ToArray());
            // await Task.WhenAll(taskList);

            return Ok();
        }

        private async Task<int> foo(int i) {
            await Task.Delay(4000);
            return i;
        }
        // private int bar(int i) {
        //     Thread.Sleep(4000);
        //     return i;
        // }
        private Task<int> bar(int i) {
            // return Task.Factory.StartNew(() => {
            //     Thread.Sleep(4000);
            //     return i;
            // });
            return Task.Run(() => {
                Thread.Sleep(4000);
                return i;
            });
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
             return CreatedAtAction(
                nameof(Post), 
                nameof(DepartmentsController), 
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