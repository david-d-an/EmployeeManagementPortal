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
//     public class EmployeeDetailProcessorTest
//     {
//         private int empNo;
//         private int managerEmpNo;
//         private string deptNo;
//         private int salary;
//         private EmployeeDetail employeeDtail;
//         private int newEmpNo;
//         private int invalidEmpNo;
//         private EmployeeDetail employeeDetail;
//         private Mock<IEmployeeDetailRepository> mockEmployeeDetailRepository;
//         private EmployeeDetailProcessor _processor;

//         public EmployeeDetailProcessorTest()
//         {
//             empNo = 0;
//             managerEmpNo = 99;
//             deptNo = "0";
//             salary = 10000;
//             employeeDtail = 
//                 new EmployeeDetail {
//                     EmpNo = empNo,
//                     BirthDate = DateTime.Now,
//                     FirstName = string.Empty,
//                     LastName = string.Empty,
//                     Gender = string.Empty,
//                     HireDate = DateTime.Now,
//                     DeptNo = deptNo,
//                     DeptName = string.Empty,
//                     ManagerEmpNo =  managerEmpNo,
//                     ManagerFirstName = string.Empty,
//                     ManagerLastName = string.Empty,
//                     Salary = salary,
//                     Title = string.Empty
//                 };

//             mockEmployeeDetailRepository = new Mock<IEmployeeDetailRepository>();
//             mockEmployeeDetailRepository
//                 .Setup(x => x.Get(It.Is<int>(x => x == empNo)))
//                 .Returns(employeeDtail);

//             _processor = new EmployeeDetailProcessor(mockEmployeeDetailRepository.Object);
//         }
        
//         [Fact]
//         public void ShouldReturnEmployeeDetailByEmpNo() {
//             // Arrange

//             // Act
//             EmployeeDetail searchResult = _processor.GetEmployeeDetailByEmpNo(empNo);

//             // Assert
//             Assert.NotNull(searchResult);
//             Assert.Equal(searchResult.EmpNo, empNo);
//         }

//     }
// }