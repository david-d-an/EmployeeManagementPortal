using System.Collections.Generic;
using System.Linq;
using EMP.Data.Repos;
using EMP.Data.Models;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EMP.Api.Controllers
{
    public class DepartmentsControllerTest
    {
        private Departments department;
        private Mock<ILogger<DepartmentsController>> mockLogger;
        private Mock<IRepository<Departments>> mockDepartmentsRepository;
        private string deptNo;
        private string invalidDeptNo;
        private DepartmentsController _controller;
        private string newDeptNo;
        public DepartmentsControllerTest()
        {
            deptNo = "0";
            newDeptNo = "1";
            invalidDeptNo = "-1";

            department = new Departments {
                DeptNo = deptNo,
                DeptName = "Existing Dept"
            };

            mockLogger = new Mock<ILogger<DepartmentsController>>();

            mockDepartmentsRepository = new Mock<IRepository<Departments>>();
            mockDepartmentsRepository.Setup(x => x.GetAsync(deptNo)).ReturnsAsync(department);
            mockDepartmentsRepository.Setup(x => x.GetAsync(invalidDeptNo)).ReturnsAsync((Departments)null);

            _controller = new DepartmentsController(
                mockLogger.Object, 
                mockDepartmentsRepository.Object);
        }

        [Fact]
        public async void ShouldReturnAllDeptManagers() {
            // Arrange
            var departments = new List<Departments> {
                department
            };

            mockDepartmentsRepository.Setup(x => x.GetAsync(null, null)).Returns(departments);

            // Act
            ActionResult<IEnumerable<Departments>> searchResult = await _controller.Get();
            OkObjectResult listResult = searchResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, listResult.StatusCode);
            IEnumerable<Departments> list = listResult.Value as IEnumerable<Departments>;
            Assert.Single(list);
            Assert.NotNull(list.FirstOrDefault());
            Assert.Equal(deptNo, list.FirstOrDefault().DeptNo);
        }

        [Fact]
        public async void ShouldReturnDepartmentWithDeptNo() {
            // Arrange

            // Act
            ActionResult<Departments> searchResult = await _controller.Get(deptNo);

            // Assert
            Assert.Equal(searchResult.Value.DeptNo, deptNo);
        }

        [Fact]
        public async void ShouldReturnNoDepartmentsWithInvalidDeptNo() {
            // Arrange

            // Act
            ActionResult<Departments> searchResult = await _controller.Get(invalidDeptNo);

            // Assert
            Assert.Null(searchResult.Value);
        }

        [Fact]
        public async void ShouldUpdateDepartmentInfo()
        {
            // Arrange
            Departments departmentUpdateRequest = 
                new Departments {
                    DeptNo = deptNo,
                    DeptName = "New Department"
                };

            mockDepartmentsRepository
                .Setup(x => x.PutAsync(deptNo, departmentUpdateRequest))
                .ReturnsAsync(departmentUpdateRequest);

            // // Pre Assert
            // ActionResult<Departments> existingDepartments = await _controller.Get(deptNo);
            // Assert.NotNull(existingDepartments.Value);

            // Act
            ActionResult<Departments> updateResult = await _controller.Put(deptNo, departmentUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;

            // Assert
            Assert.Null(notFoundResult);
            Assert.Equal(departmentUpdateRequest.DeptNo, updateResult.Value.DeptNo);
            Assert.Equal(departmentUpdateRequest.DeptName, updateResult.Value.DeptName);
        }
 
         [Fact]
        public async void ShouldCreateDepartment()
        {
            // Arrange
            Departments departmentCreateRequest = 
                new Departments {
                    DeptNo = null,
                    DeptName = "New Department"
                };

            Departments newDepartment = 
                new Departments {
                    DeptNo = newDeptNo,
                    DeptName = "New Department"
                };

            mockDepartmentsRepository
                .Setup(x => x.PostAsync(departmentCreateRequest))
                .ReturnsAsync(newDepartment);

            // Pre Assert
            ActionResult<Departments> existingDepartment = await _controller.Get(newDeptNo);
            Assert.Null(existingDepartment.Value);

            // Act
            ActionResult<Departments> postResult = await _controller.Post(departmentCreateRequest);
            CreatedAtActionResult createdResult = postResult.Result as CreatedAtActionResult;
            Departments createdDepartment = createdResult.Value as Departments;
            
            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            Assert.Equal(departmentCreateRequest.DeptName, createdDepartment.DeptName);
        }
    }
}