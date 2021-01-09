using System;
using System.Collections.Generic;
using System.Linq;
using EMP.Data.Repos;
using EMP.Data.Models;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EMP.Data.Models.Mapped;

namespace EMP.Api.Controllers
{
    public class DeptManagerControllerTest
    {
        private int empNo;
        private string deptNo;
        private string invalidDeptNo;
        private DeptManager deptManager;
        private Employees employee;
        private Departments department;
        private Mock<ILogger<DeptManagerController>> mockLogger;
        private Mock<IRepository<DeptManager>> mockDeptManagerRepository;
        private DeptManagerController _controller;
        private Mock<IRepository<Employees>> mockEmployeeRepository;
        private Mock<IRepository<Departments>> mockDepartmentsRepository;

        public DeptManagerControllerTest()
        {
            empNo = 0;
            deptNo = "d0000";
            invalidDeptNo = "xxxxx";

            deptManager = 
                new DeptManager {
                    EmpNo = empNo,
                    DeptNo = deptNo,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now
                 };

            employee = 
                new Employees {
                    EmpNo = empNo,
                    BirthDate = DateTime.Now,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Gender = string.Empty,
                    HireDate = DateTime.Now
                };

            department = 
                new Departments {
                    DeptNo = deptNo,
                    DeptName = "Existing Dept"
                 };


            var deptManagers = new List<DeptManager> {
                deptManager
            };
            var employees = new List<Employees> {
                employee
            };
            var departments = new List<Departments> {
                department
            };

            mockLogger = new Mock<ILogger<DeptManagerController>>();

            mockDeptManagerRepository = new Mock<IRepository<DeptManager>>();
            mockEmployeeRepository = new Mock<IRepository<Employees>>();
            mockDepartmentsRepository = new Mock<IRepository<Departments>>();

            // Set up for Get(id) method
            mockDeptManagerRepository.Setup(x => x.GetAsync(deptNo)).ReturnsAsync(deptManager);
            mockDeptManagerRepository.Setup(x => x.GetAsync(invalidDeptNo)).ReturnsAsync((DeptManager)null);

            // Set up for Get() method
            mockDeptManagerRepository.Setup(x => x.GetAsync(null, null)).ReturnsAsync(deptManagers);
            mockEmployeeRepository.Setup(x => x.GetAsync(null, null)).ReturnsAsync(employees);
            mockDepartmentsRepository.Setup(x => x.GetAsync(null, null)).ReturnsAsync(departments);

            _controller = new DeptManagerController(
                mockLogger.Object,
                mockDeptManagerRepository.Object,
                mockEmployeeRepository.Object,
                mockDepartmentsRepository.Object);            
        }

        [Fact]
        public async void ShouldReturnAllDeptManagers() {
            // Arrange

            // Act
            ActionResult<IEnumerable<DepartmentManagerDetail>> searchResult = await _controller.Get();
            OkObjectResult listResult = searchResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, listResult.StatusCode);
            IEnumerable<DepartmentManagerDetail> list = listResult.Value as IEnumerable<DepartmentManagerDetail>;
            Assert.Single(list);
            Assert.NotNull(list.FirstOrDefault());
            Assert.Equal(empNo, list.FirstOrDefault().EmpNo);
        }

        [Fact]
        public async void ShouldReturnDeptManagerWithEmpNo() {
            // Arrange

            // Act
            ActionResult<DepartmentManagerDetail> searchResult = await _controller.Get(deptNo);

            // Assert
            Assert.Equal(searchResult.Value.DeptNo, deptNo);
            Assert.Equal(searchResult.Value.EmpNo, empNo);
        }

        [Fact]
        public async void ShouldReturnNoDeptManagerWithInvalidEmpNo() {
            // Arrange

            // Act
            ActionResult<DepartmentManagerDetail> searchResult = await _controller.Get(invalidDeptNo);

            // Assert
            Assert.Null(searchResult.Value);
        }
    }
}
