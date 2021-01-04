
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
using System;

namespace EMP.Api.Controllers
{
    public class DepartmentManagerDetailControllerTest {
        private int empNo;
        private int invalidEmpNo;
        private string deptNo;
        private string invalidDeptNo;
        private DateTime dateTime;
        private DateTime fromDate;
        private DateTime toDate;
        private Mock<ILogger<DeptManagerDetailController>> mockLogger;
        private Mock<IEmployeeRepository> mockEmployeeRepository;
        private Mock<IDeptEmpRepository> mockDeptEmpRepository;
        private Mock<ITitleRepository> mockTitleRepository;
        private Mock<IDeptManagerCurrentRepository> mockDeptManagerCurrentRepository;
        private Mock<IDepartmentsRepository> mockDepartmentsRepository;
        private DeptManagerDetailController _controller;
        private string firstName;
        private string lastName;
        private string gneder;
        private string deptName;

        public DepartmentManagerDetailControllerTest()
        {
            empNo = 0;
            invalidEmpNo = -1;
            deptNo = "d001";
            invalidDeptNo = "x000";
            dateTime = DateTime.Now;
            fromDate = dateTime.AddMonths(-6);
            toDate = dateTime.AddMonths(6);
            firstName = "John";
            lastName = "Smith";
            gneder = "Male";
            deptName = "Existing Department";

            mockLogger = new Mock<ILogger<DeptManagerDetailController>>();

            // mockEmployeeDetailRepository = new Mock<IEmployeeDetailRepository>();
            mockEmployeeRepository = new Mock<IEmployeeRepository>();
            mockDeptEmpRepository = new Mock<IDeptEmpRepository>();
            // mockSalaryRepository = new Mock<ISalaryRepository>();
            mockTitleRepository = new Mock<ITitleRepository>();
            mockDeptManagerCurrentRepository = new Mock<IDeptManagerCurrentRepository>();
            mockDepartmentsRepository = new Mock<IDepartmentsRepository>();

            var listDepartments = new List<Departments>{ 
                new Departments { 
                    DeptNo = deptNo,
                    DeptName = deptName
                }
            };
            var listDeptManagerCurrent = new List<VwDeptManagerCurrent>{ 
                new VwDeptManagerCurrent { 
                    DeptNo = deptNo, 
                    EmpNo = empNo,
                    FromDate = fromDate,
                    ToDate = toDate,
                }
            };
            var listEmployees = new List<Employees>{ 
                new Employees { 
                    EmpNo = empNo,
                    BirthDate = dateTime.AddYears(-50),
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gneder,
                    HireDate = dateTime.AddYears(-1)
                }
            };

            // Arrange
            mockDepartmentsRepository
                .Setup(x => x.GetAsync())
                .ReturnsAsync(listDepartments);
            mockDeptManagerCurrentRepository
                .Setup(x => x.GetAsync())
                .ReturnsAsync(listDeptManagerCurrent);
            mockEmployeeRepository
                .Setup(x => x.GetAsync())
                .ReturnsAsync(listEmployees);

            _controller = new DeptManagerDetailController(
                mockLogger.Object,
                mockEmployeeRepository.Object,
                mockDeptEmpRepository.Object,
                mockTitleRepository.Object,
                mockDeptManagerCurrentRepository.Object,
                mockDepartmentsRepository.Object);
        }

        [Fact]
        public async void ShouldReturnAllDeptMangerDetails() {

            var listDepartments = new List<Departments>{ new Departments { DeptNo = deptNo }};
            var listDeptManagerCurrent = new List<VwDeptManagerCurrent>{ new VwDeptManagerCurrent { DeptNo = deptNo, EmpNo = empNo }};
            var listEmployees = new List<Employees>{ new Employees { EmpNo = empNo }};

            // Arrange
            mockDepartmentsRepository
                .Setup(x => x.GetAsync())   //It.Is<string>(i => i == deptNo)))
                .ReturnsAsync(listDepartments);
            mockDeptManagerCurrentRepository
                .Setup(x => x.GetAsync())
                .ReturnsAsync(listDeptManagerCurrent);
            mockEmployeeRepository
                .Setup(x => x.GetAsync())
                .ReturnsAsync(listEmployees);
            // mockDeptManagerCurrentRepository
            //     .Setup(x => x.GetAsync(It.Is<string>(i => i == deptNo)))
            //     .ReturnsAsync(new VwDeptManagerCurrent { DeptNo = deptNo, EmpNo = empNo });
            // mockEmployeeRepository
            //     .Setup(x => x.GetAsync(It.Is<int>(i => i == empNo)))
            //     .ReturnsAsync(new Employees { EmpNo = empNo });

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
        public async void ShouldReturnDeptMangerDetailForValidDeptNo() {

            // Arrange

            // Act
            ActionResult<DepartmentManagerDetail> searchResult = await _controller.Get(deptNo);

            // Assert
            Assert.Equal(empNo, searchResult.Value.EmpNo);
            Assert.Equal(deptNo, searchResult.Value.DeptNo);
            Assert.Equal(firstName, searchResult.Value.FirstName);
            Assert.Equal(lastName, searchResult.Value.LastName);
            Assert.Equal(fromDate, searchResult.Value.FromDate);
            Assert.Equal(toDate, searchResult.Value.ToDate);
            Assert.Equal(toDate, searchResult.Value.ToDate);
        }

        [Fact]
        public async void ShouldupdateDeptManagerDetailForValidDeptNo() {
            
        }

    }
}