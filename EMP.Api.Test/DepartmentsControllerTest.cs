using System.Collections.Generic;
using System.Linq;
using EMP.Api.Controllers;
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
        private Mock<IDepartmentsRepository> mockDepartmentsRepository;
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

            mockDepartmentsRepository = new Mock<IDepartmentsRepository>();
            mockDepartmentsRepository.Setup(x => x.GetAsync(deptNo)).ReturnsAsync(department);
            mockDepartmentsRepository.Setup(x => x.GetAsync(invalidDeptNo)).ReturnsAsync((Departments)null);

            _controller = new DepartmentsController(
                mockLogger.Object, 
                mockDepartmentsRepository.Object);
        }

        [Fact]
        public void ShouldReturnAllDeptManagers() {
            // Arrange
            var departments = new List<Departments> {
                department
            };

            mockDepartmentsRepository.Setup(x => x.GetAsync()).ReturnsAsync(departments);

            // Act
            Task<IEnumerable<Departments>> searchResult = _controller.Get();

            // Assert
            Assert.NotNull(searchResult.Result);
            Assert.Single(searchResult.Result);
            Assert.NotNull(searchResult.Result.FirstOrDefault());
            Assert.Equal(searchResult.Result.FirstOrDefault().DeptNo, deptNo);
        }

        [Fact]
        public void ShouldReturnDepartmentWithDeptNo() {
            // Arrange

            // Act
            Task<Departments> searchResult = _controller.Get(deptNo);

            // Assert
            Assert.Equal(searchResult.Result.DeptNo, deptNo);
        }

        [Fact]
        public void ShouldReturnNoDepartmentsWithInvalidDeptNo() {
            // Arrange

            // Act
            Task<Departments> searchResult = _controller.Get(invalidDeptNo);

            // Assert
            Assert.Null(searchResult.Result);
        }

        [Fact]
        public void ShouldUpdateDepartmentInfo()
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

            // Pre Assert
            Task<Departments> existingDepartments = _controller.Get(deptNo);
            Assert.NotNull(existingDepartments.Result);

            // Act
            Task<Departments> updateResult = _controller.Put(deptNo, departmentUpdateRequest);

            // Assert
            Assert.NotNull(updateResult.Result);
            Assert.Equal(updateResult.Result.DeptNo, departmentUpdateRequest.DeptNo);
            Assert.Equal(updateResult.Result.DeptName, departmentUpdateRequest.DeptName);
        }
 
         [Fact]
        public void ShouldCreateDepartment()
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
            Task<Departments> existingDepartment = _controller.Get(newDeptNo);
            Assert.Null(existingDepartment.Result);

            // Act
            Task<Departments> createResult = _controller.Post(departmentCreateRequest);

            // Assert
            Assert.NotNull(createResult.Result);
            // Assert.NotEqual(createResult.DeptNo, newDepartment.DeptNo);
            Assert.Equal(createResult.Result.DeptName, departmentCreateRequest.DeptName);
        }
    }
}