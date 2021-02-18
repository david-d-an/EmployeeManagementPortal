using Moq;
using Xunit;
using EMP.Api.Controllers;
using EMP.Data.Repos;
using EMP.Data.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace EMP.Api.Controllers
{
    public class EmployeeControllerTest
    {
        private int empNo;
        private int newEmpNo;
        private int invalidEmpNo;
        private Employees employee;
        private Mock<ILogger<EmployeeController>> mockLogger;
        private Mock<IRepository<Employees>> mockEmployeeRepository;
        private EmployeeController _controller;

        public EmployeeControllerTest()
        {
            empNo = 0;
            newEmpNo = 1;
            invalidEmpNo = -1;
            employee = 
                new Employees {
                    EmpNo = empNo,
                    BirthDate = DateTime.Now,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    Gender = string.Empty,
                    HireDate = DateTime.Now
                };

            mockLogger = new Mock<ILogger<EmployeeController>>();

            mockEmployeeRepository = new Mock<IRepository<Employees>>();
            mockEmployeeRepository.Setup(x => x.GetAsync(It.Is<string>(x => x == empNo.ToString())))
                                  .ReturnsAsync(employee);
            mockEmployeeRepository.Setup(x => x.DeleteAsync(invalidEmpNo.ToString()))
                                  .ReturnsAsync((Employees)null);

            _controller = new EmployeeController(
                mockLogger.Object,
                mockEmployeeRepository.Object);
        }

        [Fact]
        public async void ShouldReturnAllEmployees() {
            // Arrange
            var employees = new List<Employees> {
                employee
            };

            mockEmployeeRepository.Setup(x => x.GetAsync(null, null, null)).Returns(employees);

            // Act
            ActionResult<IEnumerable<Employees>> searchResult = await _controller.Get();
            OkObjectResult listResult = searchResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, listResult.StatusCode);
            IEnumerable<Employees> list = listResult.Value as IEnumerable<Employees>;
            Assert.Single(list);
            Assert.NotNull(list.FirstOrDefault());
            Assert.Equal(empNo, list.FirstOrDefault().EmpNo);
        }

        [Fact]
        public async void ShouldReturnEmployeeWithEmployeeNumber()
        {
            // Arrange

            // Act
            ActionResult<Employees> searchResult = await _controller.Get(empNo);

            // Assert
            Assert.Equal(empNo, searchResult.Value.EmpNo);
        }

        [Fact]
        public async void ShouldReturnNoEmployeesWithInvalidEmpNo() {
            // Arrange

            // Act
            ActionResult<Employees> searchResult = await _controller.Get(invalidEmpNo);

            // Assert
            Assert.Null(searchResult.Value);
        }

        [Fact]
        public async void ShouldUpdateEmployeeInfo()
        {
            // Arrange
            Employees employeeUpdateRequest = 
                new Employees {
                    EmpNo = empNo,
                    BirthDate = DateTime.Now.AddDays(-1000),
                    FirstName = "Jane",
                    LastName = "Doe",
                    Gender = "Female",
                    HireDate = DateTime.Now.AddDays(-1000)
                };

            mockEmployeeRepository.Setup(x => x.PutAsync(empNo.ToString(), employeeUpdateRequest))
                                  .ReturnsAsync(employeeUpdateRequest);

            // // Pre Assert
            // ActionResult<Employees> existingEmployee = await _controller.Get(empNo);
            // // Assert.NotNull(existingEmployee.Result);
            // Assert.NotNull(existingEmployee.Value);

            // Act
            ActionResult<Employees> updateResult = await _controller.Put(empNo, employeeUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;

            // Assert
            Assert.Null(notFoundResult);
            Assert.NotNull(updateResult.Value);
            Assert.Equal(employeeUpdateRequest.EmpNo, updateResult.Value.EmpNo);
            Assert.Equal(employeeUpdateRequest.BirthDate, updateResult.Value.BirthDate);
            Assert.Equal(employeeUpdateRequest.FirstName, updateResult.Value.FirstName);
            Assert.Equal(employeeUpdateRequest.LastName, updateResult.Value.LastName);
            Assert.Equal(employeeUpdateRequest.Gender, updateResult.Value.Gender);
            Assert.Equal(employeeUpdateRequest.HireDate, updateResult.Value.HireDate);
        }
 
         [Fact]
        public async void ShouldCreateEmployee()
        {
            // Arrange
            EmployeeRequest employeeCreateRequest = 
                new EmployeeRequest {
                    EmpNo = null,
                    BirthDate = DateTime.MinValue,
                    FirstName = "John",
                    LastName = "Smith",
                    Gender = "Male",
                    HireDate = DateTime.MinValue
                };

            Employees newEmployee = 
                new Employees {
                    EmpNo = newEmpNo,
                    BirthDate = DateTime.MinValue,
                    FirstName = "John",
                    LastName = "Smith",
                    Gender = "Male",
                    HireDate = DateTime.MinValue
                };

            mockEmployeeRepository
                .Setup(x => x.PostAsync(It.Is<EmployeeRequest>(x => x == employeeCreateRequest)))
                .ReturnsAsync(newEmployee);

            // Pre Assert
            ActionResult<Employees> existingEmployee = _controller.Get(newEmpNo).Result;
            Assert.Null(existingEmployee.Value);

            // Act
            ActionResult<Employees> postResult = await _controller.Post(employeeCreateRequest);
            CreatedAtActionResult createdResult = postResult.Result as CreatedAtActionResult;
            Employees createdEmployee = createdResult.Value as Employees;
            
            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            // Assert.NotEqual(createResult.EmpNo, employeeCreateRequest.EmpNo);
            Assert.Equal(employeeCreateRequest.BirthDate, createdEmployee.BirthDate);
            Assert.Equal(employeeCreateRequest.FirstName, createdEmployee.FirstName);
            Assert.Equal(employeeCreateRequest.LastName, createdEmployee.LastName);
            Assert.Equal(employeeCreateRequest.Gender, createdEmployee.Gender);
            Assert.Equal(employeeCreateRequest.HireDate, createdEmployee.HireDate);
        }

        [Fact]
        public async void ShouldDeleteEmployee()
        {
            // Arrange
            mockEmployeeRepository.Setup(x => x.DeleteAsync(empNo.ToString()))
                                  .ReturnsAsync((Employees)null);

            // Act
            ActionResult<Employees> deleteResult = await _controller.Delete(empNo);

            // Assert
            NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;
            Assert.Null(notFoundResult);
            Assert.Null(deleteResult.Value);
        } 

        [Fact]
        public async void ShouldReturnNotFoundWhenDeleteEmployeeWithInvalidEmpNo()
        {
            // Arrange

            // Act
            ActionResult<Employees> deleteResult = await _controller.Delete(invalidEmpNo);
            NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;

            // Assert
            Assert.Equal(404, notFoundResult.StatusCode);
        }
    }
}