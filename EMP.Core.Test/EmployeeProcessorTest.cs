// using Moq;
// using Xunit;
// using EMP.Core.Processors;
// using EMP.Core.Repos;
// using EMP.Data.Models;
// using System;
// using System.Collections.Generic;
// using System.Linq;

// namespace EMP.Core
// {
//     public class EmployeeProcessorTest
//     {
//         private int empNo;
//         private int newEmpNo;
//         private int invalidEmpNo;
//         private Employees employee;
//         private Mock<IEmployeeRepository> mockEmployeeRepository;
//         private EmployeeProcessor _processor;

//         public EmployeeProcessorTest()
//         {
//             empNo = 0;
//             newEmpNo = 1;
//             invalidEmpNo = -1;
//             employee = 
//                 new Employees {
//                     EmpNo = empNo,
//                     BirthDate = DateTime.Now,
//                     FirstName = string.Empty,
//                     LastName = string.Empty,
//                     Gender = string.Empty,
//                     HireDate = DateTime.Now
//                 };

//             mockEmployeeRepository = new Mock<IEmployeeRepository>();
//             mockEmployeeRepository.Setup(x => x.Get(It.Is<int>(x => x == empNo)))
//                                   .Returns(employee);

//             _processor = new EmployeeProcessor(mockEmployeeRepository.Object);
//         }

//         [Fact]
//         public void ShouldReturnAllEmployees() {
//             // Arrange
//             var employees = new List<Employees> {
//                 employee
//             };

//             mockEmployeeRepository.Setup(x => x.Get()).Returns(employees);

//             // Act
//             IEnumerable<Employees> searchResult = _processor.GetAllEmployees();

//             // Assert
//             Assert.NotNull(searchResult);
//             Assert.Single(searchResult);
//             Assert.NotNull(searchResult.FirstOrDefault());
//             Assert.Equal(searchResult.FirstOrDefault().EmpNo, empNo);
//         }

//         [Fact]
//         public void ShouldReturnEmployeeWithEmployeeNumber()
//         {
//             // Arrange

//             // Act
//             Employees searchResult = _processor.GetEmployeeByEmpNo(empNo);

//             // Assert
//             Assert.Equal(searchResult.EmpNo, empNo);
//         }

//         [Fact]
//         public void ShouldUpdateEmployeeInfo()
//         {
//             // Arrange
//             Employees employeeUpdateRequest = 
//                 new Employees {
//                     EmpNo = empNo,
//                     BirthDate = DateTime.Now.AddDays(-1000),
//                     FirstName = "Jane",
//                     LastName = "Doe",
//                     Gender = "Female",
//                     HireDate = DateTime.Now.AddDays(-1000)
//                 };

//             mockEmployeeRepository.Setup(x => x.Put(employeeUpdateRequest)).Returns(employeeUpdateRequest);

//             // Pre Assert
//             Employees existingEmployee = _processor.GetEmployeeByEmpNo(empNo);
//             Assert.NotNull(existingEmployee);

//             // Act
//             Employees updateResult = _processor.UpdateEmployeeInfo(employeeUpdateRequest);

//             // Assert
//             Assert.NotNull(updateResult);
//             Assert.Equal(updateResult.EmpNo, employeeUpdateRequest.EmpNo);
//             Assert.Equal(updateResult.BirthDate, employeeUpdateRequest.BirthDate);
//             Assert.Equal(updateResult.FirstName, employeeUpdateRequest.FirstName);
//             Assert.Equal(updateResult.LastName, employeeUpdateRequest.LastName);
//             Assert.Equal(updateResult.Gender, employeeUpdateRequest.Gender);
//             Assert.Equal(updateResult.HireDate, employeeUpdateRequest.HireDate);
//         }
 
//          [Fact]
//         public void ShouldCreateEmployee()
//         {
//             // Arrange
//             EmployeeRequest employeeCreateRequest = 
//                 new EmployeeRequest {
//                     EmpNo = null,
//                     BirthDate = DateTime.MinValue,
//                     FirstName = "John",
//                     LastName = "Smith",
//                     Gender = "Male",
//                     HireDate = DateTime.MinValue
//                 };

//             Employees newEmployee = 
//                 new Employees {
//                     EmpNo = newEmpNo,
//                     BirthDate = DateTime.MinValue,
//                     FirstName = "John",
//                     LastName = "Smith",
//                     Gender = "Male",
//                     HireDate = DateTime.MinValue
//                 };

//             mockEmployeeRepository
//                 .Setup(x => x.Post(It.Is<EmployeeRequest>(x => x == employeeCreateRequest)))
//                 .Returns(newEmployee);

//             // Pre Assert
//             Employees existingEmployee = _processor.GetEmployeeByEmpNo(newEmpNo);
//             Assert.Null(existingEmployee);

//             // Act
//             Employees createResult = _processor.CreateEmployee(employeeCreateRequest);

//             // Assert
//             Assert.NotNull(createResult);
//             // Assert.NotEqual(createResult.EmpNo, employeeCreateRequest.EmpNo);
//             Assert.Equal(createResult.BirthDate, employeeCreateRequest.BirthDate);
//             Assert.Equal(createResult.FirstName, employeeCreateRequest.FirstName);
//             Assert.Equal(createResult.LastName, employeeCreateRequest.LastName);
//             Assert.Equal(createResult.Gender, employeeCreateRequest.Gender);
//             Assert.Equal(createResult.HireDate, employeeCreateRequest.HireDate);
//         }

//          [Fact]
//         public void ShouldDeleteEmployee()
//         {
//             // Arrange
//             mockEmployeeRepository.Setup(x => x.Delete(empNo)).Returns((Employees)null);

//             // Act
//             Employees deleteResult = _processor.DeleteEmployee(empNo);

//             // Assert
//             Assert.Null(deleteResult);
//         } 

//         [Fact]
//         public void ShouldReturnNoEmployeesWithInvalidEmpNo() {
//             // Arrange

//             // Act
//             Employees searchResult = _processor.GetEmployeeByEmpNo(invalidEmpNo);

//             // Assert
//             Assert.Null(searchResult);
//         }
//     }
// }