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


namespace EMP.Api.Controllers
{
    public class EmployeeDetailControllerTest
    {
        private int empNo;
        private int managerEmpNo;
        private string deptNo;
        private int salary;
        private VwEmpDetails vwEmpDetails;
        private Mock<ILogger<EmployeeDetailController>> mockLogger;
        private int invalidEmpNo;
        private Mock<IEmployeeDetailRepository> mockEmployeeDetailRepository;
        private Mock<IEmployeeRepository> mockEmployeeRepository;
        private Mock<IDeptEmpRepository> mockDeptEmpRepository;
        private Mock<ISalaryRepository> mockSalaryRepository;
        private Mock<ITitleRepository> mockTitleRepository;
        private EmployeeDetailController _controller;
        private int newEmpNo;

        public EmployeeDetailControllerTest()
        {
            empNo = 0;
            invalidEmpNo = -1;
            newEmpNo = 199;
            managerEmpNo = 99;
            deptNo = "0";
            salary = 10000;

            vwEmpDetails = 
                new VwEmpDetails {
                    EmpNo = empNo,
                    FirstName = string.Empty,
                    LastName = string.Empty,
                    BirthDate = DateTime.Now,
                    HireDate = DateTime.Now,
                    Gender = string.Empty,
                    Salary = salary,
                    Title = string.Empty,
                    DeptNo = deptNo,
                    DeptName = string.Empty,
                    ManagerFirstName = string.Empty,
                    ManagerLastName = string.Empty,
                    ManagerEmpNo =  managerEmpNo
                };


            mockLogger = new Mock<ILogger<EmployeeDetailController>>();

            mockEmployeeDetailRepository = new Mock<IEmployeeDetailRepository>();
            mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockDeptEmpRepository = new Mock<IDeptEmpRepository>();
            mockSalaryRepository = new Mock<ISalaryRepository>();
            mockTitleRepository = new Mock<ITitleRepository>();

            mockEmployeeDetailRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNo.ToString())))
                .ReturnsAsync(vwEmpDetails);

            _controller = new EmployeeDetailController(
                mockLogger.Object,
                mockEmployeeRepository.Object,
                mockEmployeeDetailRepository.Object,
                mockDeptEmpRepository.Object,
                mockSalaryRepository.Object,
                mockTitleRepository.Object);
        }
        
        [Fact]
        public async void ShouldReturnAllEmployees() {
            // Arrange
            var listEmployeeDetails = new List<VwEmpDetails> {
                vwEmpDetails
            };

            mockEmployeeDetailRepository.Setup(x => x.GetAsync())
                                        .ReturnsAsync(listEmployeeDetails);

            // Act
            ActionResult<IEnumerable<VwEmpDetails>> searchResult = await _controller.Get();
            OkObjectResult listResult = searchResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, listResult.StatusCode);
            IEnumerable<VwEmpDetails> list = listResult.Value as IEnumerable<VwEmpDetails>;
            Assert.Single(list);
            Assert.NotNull(list.FirstOrDefault());
            Assert.Equal(empNo, list.FirstOrDefault().EmpNo);
        }

        [Fact]
        public async void ShouldReturnEmployeeDetailByEmpNo() {
            // Arrange

            // Act
            ActionResult<VwEmpDetails> searchResult = await _controller.Get(empNo);

            // Assert
            Assert.NotNull(searchResult.Value);
            Assert.Equal(empNo, searchResult.Value.EmpNo);
        }

        [Fact]
        public async void ShouldReturnNoEmployeesWithInvalidEmpNo() {
            // Arrange

            // Act
            ActionResult<VwEmpDetails> searchResult = await _controller.Get(invalidEmpNo);

            // Assert
            Assert.Null(searchResult.Value);
        }

        [Fact]
        public async void ShouldUpdateEmployeeInfo()
        {
            int empNoUpdate = 1;
            DateTime dateTimeNow = DateTime.Now;
            string newTitle = "New Title";
            string newDeptName = "New Department";
            int newSalary = 99999;
            string newDeptNo = "d999";

            // Arrange
            VwEmpDetails employeeUpdateRequest = 
                new VwEmpDetails {
                    EmpNo = empNoUpdate,
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = dateTimeNow.AddDays(-1000),
                    HireDate = dateTimeNow.AddDays(-1000),
                    Gender = "Female",
                    Salary = newSalary,
                    Title = newTitle,
                    DeptNo = newDeptNo,
                    DeptName = newDeptName,
                    ManagerFirstName = string.Empty,
                    ManagerLastName = string.Empty,
                    ManagerEmpNo =  managerEmpNo
                };

            Employees employee = 
                new Employees {
                    EmpNo = empNoUpdate,
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = dateTimeNow.AddDays(-1000),
                    HireDate = dateTimeNow.AddDays(-1000),
                    Gender = "Female"
                };

            mockEmployeeDetailRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNoUpdate.ToString())))
                .ReturnsAsync(employeeUpdateRequest);

            mockEmployeeRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNoUpdate.ToString())))
                .ReturnsAsync(employee);

            mockSalaryRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNoUpdate.ToString())))
                .ReturnsAsync(new VwSalariesCurrent { EmpNo = empNoUpdate, Salary = newSalary });

            mockTitleRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNoUpdate.ToString())))
                .ReturnsAsync(new VwTitlesCurrent { EmpNo = empNoUpdate, Title = newTitle });

            mockDeptEmpRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNoUpdate.ToString())))
                .ReturnsAsync(new VwDeptEmpCurrent { EmpNo = empNoUpdate, DeptNo = newDeptNo });

            // mockEmployeeDetailRepository
            //     .Setup(x => x.PutAsync(
            //         It.Is<int>(i => i == empNoUpdate), 
            //         It.Is<VwEmpDetails>(i => i == vwEmpDetails)))
            //     .ReturnsAsync(employeeUpdateRequest);

            // Act
            ActionResult<VwEmpDetails> updateResult = await _controller.Put(empNoUpdate, employeeUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;
            VwEmpDetails updatedVwEmpDetails = updateResult.Value;

            // Assert
            Assert.Null(notFoundResult);
            Assert.NotNull(updateResult.Value);
            Assert.Equal(employeeUpdateRequest.EmpNo, updatedVwEmpDetails.EmpNo);
            Assert.Equal(employeeUpdateRequest.FirstName, updatedVwEmpDetails.FirstName);
            Assert.Equal(employeeUpdateRequest.LastName, updatedVwEmpDetails.LastName);
            Assert.Equal(employeeUpdateRequest.BirthDate, updatedVwEmpDetails.BirthDate);
            Assert.Equal(employeeUpdateRequest.HireDate, updatedVwEmpDetails.HireDate);
            Assert.Equal(employeeUpdateRequest.Gender, updatedVwEmpDetails.Gender);

            Assert.Equal(employeeUpdateRequest.Salary, updatedVwEmpDetails.Salary);
            Assert.Equal(employeeUpdateRequest.Title, updatedVwEmpDetails.Title);
            Assert.Equal(employeeUpdateRequest.DeptNo, updatedVwEmpDetails.DeptNo);
        }

        [Fact]
        public async void ShouldReturnNotFoundWhenUpdatingWithInvalideEmpNo()
        {
            // Arrange
            VwEmpDetails invalidEmployeeUpdateRequest = 
                new VwEmpDetails {
                    EmpNo = invalidEmpNo,
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = DateTime.Now.AddDays(-1000),
                    HireDate = DateTime.Now.AddDays(-1000),
                    Gender = "Female",
                    Salary = 99999,
                    Title = "New Title",
                    DeptNo = "d999",
                    DeptName = "New Department",
                    ManagerFirstName = string.Empty,
                    ManagerLastName = string.Empty,
                    ManagerEmpNo =  managerEmpNo
                };

            mockEmployeeDetailRepository
                .Setup(x => x.PutAsync(invalidEmpNo.ToString(), invalidEmployeeUpdateRequest))
                .ReturnsAsync(null as VwEmpDetails);

            // Act
            ActionResult<VwEmpDetails> updateResult = await _controller.Put(invalidEmpNo, invalidEmployeeUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;
            VwEmpDetails updatedVwEmpDetails = updateResult.Value;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Null(updateResult.Value);
        }
 
         [Fact]
        public async void ShouldCreateEmployeeWithDetails()
        {
            DateTime mockDate = DateTime.Now.AddDays(-1000);
            // Arrange
            VwEmpDetails employeeCreateRequest = 
                new VwEmpDetails {
                    EmpNo = -1,
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = mockDate,
                    HireDate = mockDate,
                    Gender = "Female",
                    Salary = 99999,
                    Title = "New Title",
                    DeptNo = "d999",
                    DeptName = "New Department",
                    ManagerFirstName = string.Empty,
                    ManagerLastName = string.Empty,
                    ManagerEmpNo =  managerEmpNo
                };

            VwEmpDetails newEmployee = 
                new VwEmpDetails {
                    EmpNo = newEmpNo,
                    FirstName = "Jane",
                    LastName = "Doe",
                    BirthDate = mockDate,
                    HireDate = mockDate,
                    Gender = "Female",
                    Salary = 99999,
                    Title = "New Title",
                    DeptNo = "d999",
                    DeptName = "New Department",
                    ManagerFirstName = string.Empty,
                    ManagerLastName = string.Empty,
                    ManagerEmpNo =  managerEmpNo
                };

            mockEmployeeDetailRepository
                .Setup(x => x.PostAsync(It.Is<VwEmpDetails>(x => x == employeeCreateRequest)))
                .ReturnsAsync(newEmployee);

            // Act
            ActionResult<VwEmpDetails> postResult = await _controller.Post(employeeCreateRequest);
            CreatedAtActionResult createdResult = postResult.Result as CreatedAtActionResult;
            VwEmpDetails createdEmployee = createdResult.Value as VwEmpDetails;
            
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

        // [Fact]
        // public async void ShouldDeleteEmployee()
        // {
        //     // Arrange
        //     mockEmployeeRepository.Setup(x => x.DeleteAsync(empNo))
        //                           .ReturnsAsync((Employees)null);

        //     // Act
        //     ActionResult<Employees> deleteResult = await _controller.Delete(empNo);

        //     // Assert
        //     NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;
        //     Assert.Null(notFoundResult);
        //     Assert.Null(deleteResult.Value);
        // } 

        // [Fact]
        // public async void ShouldReturnNotFoundWhenDeleteEmployeeWithInvalidEmpNo()
        // {
        //     // Arrange

        //     // Act
        //     ActionResult<Employees> deleteResult = await _controller.Delete(invalidEmpNo);
        //     NotFoundResult notFoundResult = deleteResult.Result as NotFoundResult;

        //     // Assert
        //     Assert.Equal(404, notFoundResult.StatusCode);
        // }
    }
}