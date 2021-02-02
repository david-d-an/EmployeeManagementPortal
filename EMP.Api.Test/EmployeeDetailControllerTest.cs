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
        private string title;
        private DateTime timeNow;
        private string gender;
        private string managerFirstName;
        private string managerLastName;
        private VwEmpDetailsShort vwEmpDetailsShort;
        private VwEmpDetails vwEmpDetails;
        private Mock<ILogger<EmployeeDetailController>> mockLogger;
        private int invalidEmpNo;
        private Mock<IRepository<VwEmpDetails>> mockEmployeeDetailRepository;
        private Mock<IRepository<VwEmpDetailsShort>> mockEmployeeDetailShortRepository;
        private Mock<IRepository<Employees>> mockEmployeeRepository;
        private Mock<IRepository<VwDeptEmpCurrent>> mockDeptEmpCurrentRepository;
        private Mock<IRepository<VwSalariesCurrent>> mockSalaryRepository;
        private Mock<IRepository<VwTitlesCurrent>> mockTitleRepository;
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
            title = "Test Title";
            timeNow = DateTime.Now;
            gender = "M";
            managerFirstName = "John";
            managerLastName = "Smith";

            vwEmpDetails = new VwEmpDetails {
                EmpNo = empNo,
                FirstName = string.Empty,
                LastName = string.Empty,
                BirthDate = timeNow,
                HireDate = timeNow,
                Gender = gender,
                Salary = salary,
                Title = title,
                DeptNo = deptNo,
                DeptName = string.Empty,
                ManagerEmpNo =  managerEmpNo,
                ManagerFirstName = managerFirstName,
                ManagerLastName = managerLastName
            };

            vwEmpDetailsShort = new VwEmpDetailsShort {
                EmpNo = empNo,
                FirstName = string.Empty,
                LastName = string.Empty,
                Title = string.Empty,
                Salary = salary,
                DeptNo = deptNo,
                DeptName = string.Empty,
                // ManagerEmpNo =  managerEmpNo
            };


            mockLogger = new Mock<ILogger<EmployeeDetailController>>();

            mockEmployeeDetailRepository = new Mock<IRepository<VwEmpDetails>>();
            mockEmployeeDetailShortRepository = new Mock<IRepository<VwEmpDetailsShort>>();
            mockEmployeeRepository = new Mock<IRepository<Employees>>();
            mockDeptEmpCurrentRepository = new Mock<IRepository<VwDeptEmpCurrent>>();
            mockSalaryRepository = new Mock<IRepository<VwSalariesCurrent>>();
            mockTitleRepository = new Mock<IRepository<VwTitlesCurrent>>();

            mockEmployeeDetailRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == empNo.ToString())))
                .ReturnsAsync(vwEmpDetails);

            _controller = new EmployeeDetailController(
                mockLogger.Object,
                mockEmployeeRepository.Object,
                mockEmployeeDetailRepository.Object,
                mockEmployeeDetailShortRepository.Object,
                mockDeptEmpCurrentRepository.Object,
                mockSalaryRepository.Object,
                mockTitleRepository.Object);
        }
        
        [Fact]
        public async void ShouldReturnAllEmployees() {
            // Arrange
            var listEmployeeDetails = new List<VwEmpDetailsShort> {
                vwEmpDetailsShort
            };

            mockEmployeeDetailShortRepository
                .Setup(x => x.GetAsync(null, null, null))
                .Returns(listEmployeeDetails);

            // Act
            ActionResult<IEnumerable<VwEmpDetails>> searchResult = 
                (await _controller.Get(null, null, null, null, null, null)).Result;
            OkObjectResult listResult = searchResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, listResult.StatusCode);
            IEnumerable<VwEmpDetailsShort> list = listResult.Value as IEnumerable<VwEmpDetailsShort>;
            Assert.Single(list);
            Assert.NotNull(list.FirstOrDefault());
            Assert.Equal(empNo, list.FirstOrDefault().EmpNo);
        }

        [Fact]
        public async void ShouldReturnEmployeeDetailByEmpNo() {

            // Arrange
            mockEmployeeDetailRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == vwEmpDetails.EmpNo.ToString())))
                .ReturnsAsync(vwEmpDetails);

            // Act
            ActionResult<VwEmpDetails> searchResult = await _controller.Get(empNo);

            // Assert
            Assert.NotNull(searchResult.Value);
            Assert.Equal(vwEmpDetails.EmpNo, searchResult.Value.EmpNo);
            Assert.Equal(vwEmpDetails.FirstName, searchResult.Value.FirstName);
            Assert.Equal(vwEmpDetails.LastName, searchResult.Value.LastName);
            Assert.Equal(vwEmpDetails.BirthDate, searchResult.Value.BirthDate);
            Assert.Equal(vwEmpDetails.HireDate, searchResult.Value.HireDate);
            Assert.Equal(vwEmpDetails.Gender, searchResult.Value.Gender);
            Assert.Equal(vwEmpDetails.Salary, searchResult.Value.Salary);
            Assert.Equal(vwEmpDetails.Title, searchResult.Value.Title);
            Assert.Equal(vwEmpDetails.DeptNo, searchResult.Value.DeptNo);
            Assert.Equal(vwEmpDetails.DeptName, searchResult.Value.DeptName);
            Assert.Equal(vwEmpDetails.ManagerEmpNo, searchResult.Value.ManagerEmpNo);
            Assert.Equal(vwEmpDetails.ManagerFirstName, searchResult.Value.ManagerFirstName);
            Assert.Equal(vwEmpDetails.ManagerLastName, searchResult.Value.ManagerLastName);
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

            mockDeptEmpCurrentRepository
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
            VwEmpDetails employeeDetailCreateRequest = 
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

            VwEmpDetails employeeDetailCreateResult = 
                new VwEmpDetails {
                    EmpNo = newEmpNo,
                    FirstName = employeeDetailCreateRequest.FirstName,
                    LastName = employeeDetailCreateRequest.LastName,
                    BirthDate = employeeDetailCreateRequest.BirthDate,
                    HireDate = employeeDetailCreateRequest.HireDate,
                    Gender = employeeDetailCreateRequest.Gender,
                    Salary = employeeDetailCreateRequest.Salary,
                    Title = employeeDetailCreateRequest.Title,
                    DeptNo = employeeDetailCreateRequest.DeptNo,
                    DeptName = employeeDetailCreateRequest.DeptName,
                    ManagerFirstName = employeeDetailCreateRequest.ManagerFirstName,
                    ManagerLastName = employeeDetailCreateRequest.ManagerLastName,
                    ManagerEmpNo =  employeeDetailCreateRequest.ManagerEmpNo
                };

            Employees newEmployee = 
                new Employees {
                    EmpNo = -1,
                    FirstName = employeeDetailCreateRequest.FirstName,
                    LastName = employeeDetailCreateRequest.LastName,
                    BirthDate = employeeDetailCreateRequest.BirthDate,
                    HireDate = employeeDetailCreateRequest.HireDate,
                    Gender = employeeDetailCreateRequest.Gender,
                };

            Employees employeeCreateResult = 
                new Employees {
                    EmpNo = newEmpNo,
                    FirstName = newEmployee.FirstName,
                    LastName = newEmployee.LastName,
                    BirthDate = newEmployee.BirthDate,
                    HireDate = newEmployee.HireDate,
                    Gender = newEmployee.Gender,
                };


            mockEmployeeRepository
                .Setup(x => x.PostAsync(It.IsAny<Employees>()))
                .ReturnsAsync(employeeCreateResult);
            mockEmployeeDetailRepository
                .Setup(x => x.GetAsync(It.Is<string>(x => x == employeeCreateResult.EmpNo.ToString())))
                .ReturnsAsync(employeeDetailCreateResult);

            // Act
            ActionResult<VwEmpDetails> postResult = await _controller.Post(employeeDetailCreateRequest);
            CreatedAtActionResult createdResult = postResult.Result as CreatedAtActionResult;
            VwEmpDetails createdEmployee = createdResult.Value as VwEmpDetails;
            
            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            // Assert.NotEqual(createResult.EmpNo, employeeCreateRequest.EmpNo);
            Assert.Equal(employeeDetailCreateRequest.BirthDate, createdEmployee.BirthDate);
            Assert.Equal(employeeDetailCreateRequest.FirstName, createdEmployee.FirstName);
            Assert.Equal(employeeDetailCreateRequest.LastName, createdEmployee.LastName);
            Assert.Equal(employeeDetailCreateRequest.Gender, createdEmployee.Gender);
            Assert.Equal(employeeDetailCreateRequest.HireDate, createdEmployee.HireDate);
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