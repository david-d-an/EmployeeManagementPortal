using Moq;
using Xunit;
using EMP.Api.Controllers;
using EMP.Data.Repos;
using EMP.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace EMP.Core
{
    public class EmployeeControllerTest
    {
        private int empNo;
        private int newEmpNo;
        private int invalidEmpNo;
        private Employees employee;
        private Mock<ILogger<EmployeeController>> mockLogger;
        private Mock<IEmployeeRepository> mockEmployeeRepository;
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

            mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockEmployeeRepository.Setup(x => x.GetAsync(It.Is<int>(x => x == empNo)))
                                  .ReturnsAsync(employee);

            _controller = new EmployeeController(
                mockLogger.Object,
                mockEmployeeRepository.Object);
        }

        [Fact]
        public void ShouldReturnAllEmployees() {
            // Arrange
            var employees = new List<Employees> {
                employee
            };

            mockEmployeeRepository.Setup(x => x.GetAsync()).ReturnsAsync(employees);

            // Act
            Task<IEnumerable<Employees>> searchResult = _controller.Get();

            // Assert
            Assert.NotNull(searchResult.Result);
            Assert.Single(searchResult.Result);
            Assert.NotNull(searchResult.Result.FirstOrDefault());
            Assert.Equal(empNo, searchResult.Result.FirstOrDefault().EmpNo);
        }

        [Fact]
        public void ShouldReturnEmployeeWithEmployeeNumber()
        {
            // Arrange

            // Act
            Task<Employees> searchResult = _controller.Get(empNo);

            // Assert
            Assert.Equal(empNo, searchResult.Result.EmpNo);
        }

        [Fact]
        public async void ShouldReturnNoEmployeesWithInvalidEmpNo() {
            // Arrange

            // Act
            Employees searchResult = await _controller.Get(invalidEmpNo);

            // Assert
            Assert.Null(searchResult);
        }

        [Fact]
        public void ShouldUpdateEmployeeInfo()
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

            mockEmployeeRepository.Setup(x => x.PutAsync(empNo, employeeUpdateRequest))
                                  .ReturnsAsync(employeeUpdateRequest);

            // Pre Assert
            Employees existingEmployee = _controller.Get(empNo).Result;
            Assert.NotNull(existingEmployee);

            // Act
            Employees updateResult = _controller.Put(empNo, employeeUpdateRequest).Result;

            // Assert
            Assert.NotNull(updateResult);
            Assert.Equal(employeeUpdateRequest.EmpNo, updateResult.EmpNo);
            Assert.Equal(employeeUpdateRequest.BirthDate, updateResult.BirthDate);
            Assert.Equal(employeeUpdateRequest.FirstName, updateResult.FirstName);
            Assert.Equal(employeeUpdateRequest.LastName, updateResult.LastName);
            Assert.Equal(employeeUpdateRequest.Gender, updateResult.Gender);
            Assert.Equal(employeeUpdateRequest.HireDate, updateResult.HireDate);
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
            Employees existingEmployee = _controller.Get(newEmpNo).Result;
            Assert.Null(existingEmployee);

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
            mockEmployeeRepository.Setup(x => x.DeleteAsync(empNo))
                                  .ReturnsAsync((Employees)null);

            // Act
            ActionResult<Employees> deleteResult = await _controller.Delete(empNo);

            // Assert
            Assert.Null(deleteResult.Value);
        } 

        [Fact]
        public async void ShouldReturenNotFoundWhenWhenDeletingInvalidEmployee()
        {
            // Arrange

            // Act
            ActionResult<Employees> deleteResult = await _controller.Delete(invalidEmpNo);
            NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        } 
    }
}