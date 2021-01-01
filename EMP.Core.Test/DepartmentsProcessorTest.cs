// using System.Collections.Generic;
// using System.Linq;
// using EMP.Core.Processors;
// using EMP.Core.Repos;
// using EMP.Data.Models;
// using Moq;
// using Xunit;

// namespace EMP.Core
// {
//     public class DepartmentsProcessorTest
//     {
//         private Departments department;
//         private Mock<IDepartmentsRepository> mockDepartmentsRepository;
//         private string deptNo;
//         private string invalidDeptNo;
//         private DepartmentsProcessor _processor;
//         private string newDeptNo;

//         public DepartmentsProcessorTest()
//         {
//             deptNo = "0";
//             newDeptNo = "1";
//             invalidDeptNo = "-1";

//             department = new Departments {
//                 DeptNo = deptNo,
//                 DeptName = string.Empty
//             };

//             mockDepartmentsRepository = new Mock<IDepartmentsRepository>();
//             mockDepartmentsRepository.Setup(x => x.Get(deptNo)).Returns(department);
//             mockDepartmentsRepository.Setup(x => x.Get(invalidDeptNo)).Returns<Departments>(null);

//             _processor = new DepartmentsProcessor(mockDepartmentsRepository.Object);
//         }

//         [Fact]
//         public void ShouldReturnAllDeptManagers() {
//             // Arrange
//             var departments = new List<Departments> {
//                 department
//             };

//             mockDepartmentsRepository.Setup(x => x.Get()).Returns(departments);

//             // Act
//             IEnumerable<Departments> searchResult = _processor.GetAllDepartments();

//             // Assert
//             Assert.NotNull(searchResult);
//             Assert.Single(searchResult);
//             Assert.NotNull(searchResult.FirstOrDefault());
//             Assert.Equal(searchResult.FirstOrDefault().DeptNo, deptNo);
//         }

//         [Fact]
//         public void ShouldReturnDepartmentWithDeptNo() {
//             // Arrange

//             // Act
//             Departments searchResult = _processor.GetDepartmentByDeptNo(deptNo);

//             // Assert
//             Assert.Equal(searchResult.DeptNo, deptNo);
//         }

//         [Fact]
//         public void ShouldReturnNoDepartmentsWithInvalidDeptNo() {
//             // Arrange

//             // Act
//             Departments searchResult = _processor.GetDepartmentByDeptNo(invalidDeptNo);

//             // Assert
//             Assert.Null(searchResult);
//         }

//         [Fact]
//         public void ShouldUpdateDepartmentInfo()
//         {
//             // Arrange
//             Departments departmentUpdateRequest = 
//                 new Departments {
//                     DeptNo = deptNo,
//                     DeptName = "New Department"
//                 };

//             mockDepartmentsRepository.Setup(x => x.Put(departmentUpdateRequest)).Returns(departmentUpdateRequest);

//             // Pre Assert
//             Departments existingDepartments = _processor.GetDepartmentByDeptNo(deptNo);
//             Assert.NotNull(existingDepartments);

//             // Act
//             Departments updateResult = _processor.UpdateDepartmentInfo(departmentUpdateRequest);

//             // Assert
//             Assert.NotNull(updateResult);
//             Assert.Equal(updateResult.DeptNo, departmentUpdateRequest.DeptNo);
//             Assert.Equal(updateResult.DeptName, departmentUpdateRequest.DeptName);
//         }
 
//          [Fact]
//         public void ShouldCreateDepartment()
//         {
//             // Arrange
//             Departments departmentCreateRequest = 
//                 new Departments {
//                     DeptNo = null,
//                     DeptName = "New Department"
//                 };

//             Departments newDepartment = 
//                 new Departments {
//                     DeptNo = newDeptNo,
//                     DeptName = "New Department"
//                 };

//             mockDepartmentsRepository.Setup(x => x.Post(departmentCreateRequest)).Returns(newDepartment);

//             // Pre Assert
//             Departments existingDepartment = _processor.GetDepartmentByDeptNo(newDeptNo);
//             Assert.Null(existingDepartment);

//             // Act
//             Departments createResult = _processor.CreateDepartment(departmentCreateRequest);

//             // Assert
//             Assert.NotNull(createResult);
//             // Assert.NotEqual(createResult.DeptNo, newDepartment.DeptNo);
//             Assert.Equal(createResult.DeptName, departmentCreateRequest.DeptName);
//         }
//     }
// }