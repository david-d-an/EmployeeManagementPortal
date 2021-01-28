
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
        private int managerEmpNo;
        private int invalidManagerEmpNo;
        private string deptNo;
        private string invalidDeptNo;
        private DateTime dateTime;
        private DateTime fromDate;
        private DateTime toDate;
        private string managerFirstName;
        private string managerLastName;
        private string managerGender;
        private Mock<ILogger<DeptManagerDetailController>> mockLogger;
        private Mock<IRepository<Employees>> mockEmployeeRepository;
        // private Mock<IRepository<VwDeptEmpCurrent>> mockDeptEmpRepository;
        private Mock<IRepository<VwTitlesCurrent>> mockTitleRepository;
        private Mock<IRepository<VwDeptManagerCurrent>> mockDeptManagerCurrentRepository;
        private Mock<IRepository<VwDeptManagerDetail>> mockDeptManagerDetailRepository;
        private Mock<IRepository<Departments>> mockDepartmentsRepository;
        private DeptManagerDetailController _controller;
        private string deptName;
        private int newManagerEmpNo;
        private string newManagerFirstName;
        private string newManagerLastName;
        private string newDeptName;
        private string newDeptNo;

        public DepartmentManagerDetailControllerTest()
        {
            managerEmpNo = 0;
            invalidManagerEmpNo = -1;
            deptNo = "d001";
            invalidDeptNo = "x000";
            dateTime = DateTime.Now;
            fromDate = dateTime.AddMonths(-6);
            toDate = dateTime.AddMonths(6);
            managerFirstName = "John";
            managerLastName = "Smith";
            managerGender = "Male";
            deptName = "Existing Department";


            newManagerEmpNo = 99;
            newManagerFirstName = "Jane";
            newManagerLastName = "Doe";
            newDeptNo = "d999";
            newDeptName = "New Department";

            mockLogger = new Mock<ILogger<DeptManagerDetailController>>();

            // mockEmployeeDetailRepository = new Mock<IEmployeeDetailRepository>();
            mockEmployeeRepository = new Mock<IRepository<Employees>>();
            // mockDeptEmpRepository = new Mock<IRepository<VwDeptEmpCurrent>>();
            // mockSalaryRepository = new Mock<ISalaryRepository>();
            mockTitleRepository = new Mock<IRepository<VwTitlesCurrent>>();
            mockDeptManagerCurrentRepository = new Mock<IRepository<VwDeptManagerCurrent>>();
            mockDeptManagerDetailRepository = new Mock<IRepository<VwDeptManagerDetail>>();
            mockDepartmentsRepository = new Mock<IRepository<Departments>>();

            var listDepartments = new List<Departments>{ 
                new Departments { 
                    DeptNo = deptNo,
                    DeptName = deptName
                }
            };
            var listDeptManagerCurrent = new List<VwDeptManagerCurrent>{ 
                new VwDeptManagerCurrent { 
                    DeptNo = deptNo, 
                    EmpNo = managerEmpNo,
                    FromDate = fromDate,
                    ToDate = toDate,
                }
            };
            var listEmployees = new List<Employees>{ 
                new Employees { 
                    EmpNo = managerEmpNo,
                    BirthDate = dateTime.AddYears(-50),
                    FirstName = managerFirstName,
                    LastName = managerLastName,
                    Gender = managerGender,
                    HireDate = dateTime.AddYears(-1)
                }
            };

            // Arrange
            mockDepartmentsRepository
                .Setup(x => x.GetAsync(null, null))
                .Returns(listDepartments);
            mockDeptManagerCurrentRepository
                .Setup(x => x.GetAsync(null, null))
                .Returns(listDeptManagerCurrent);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(null, null))
                .Returns(listEmployees);

            mockDepartmentsRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == deptNo)))
                .ReturnsAsync(listDepartments.Where(i => i.DeptNo == deptNo).FirstOrDefault());
            // mockDeptManagerCurrentRepository
            //     .Setup(x => x.GetAsync())
            //     .ReturnsAsync(listDeptManagerCurrent);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == managerEmpNo.ToString())))
                .ReturnsAsync(listEmployees.Where(i => i.EmpNo == managerEmpNo).FirstOrDefault());

            _controller = new DeptManagerDetailController(
                mockLogger.Object,
                mockEmployeeRepository.Object,
                // mockDeptEmpRepository.Object,
                // mockTitleRepository.Object,
                mockDeptManagerCurrentRepository.Object,
                mockDeptManagerDetailRepository.Object,
                mockDepartmentsRepository.Object);
        }

        [Fact]
        public async void ShouldReturnAllDeptMangerDetails() {

            var listDepartments = new List<Departments>{ new Departments { DeptNo = deptNo }};
            var listDeptManagerCurrent = 
                new List<VwDeptManagerDetail>{ 
                    new VwDeptManagerDetail { 
                        DeptNo = deptNo, 
                        EmpNo = managerEmpNo 
                    }
                };
            var listEmployees = new List<Employees>{ new Employees { EmpNo = managerEmpNo }};

            // Arrange
            mockDepartmentsRepository
                .Setup(x => x.GetAsync(null, null))   //It.Is<string>(i => i == deptNo)))
                .Returns(listDepartments);
            mockDeptManagerDetailRepository
                .Setup(x => x.GetAsync(null, null))
                .Returns(listDeptManagerCurrent);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(null, null))
                .Returns(listEmployees);
            // mockDeptManagerCurrentRepository
            //     .Setup(x => x.GetAsync(It.Is<string>(i => i == deptNo)))
            //     .ReturnsAsync(new VwDeptManagerCurrent { DeptNo = deptNo, EmpNo = empNo });
            // mockEmployeeRepository
            //     .Setup(x => x.GetAsync(It.Is<int>(i => i == empNo)))
            //     .ReturnsAsync(new Employees { EmpNo = empNo });

            // Act
            ActionResult<IEnumerable<DepartmentManagerDetail>> searchResult = (await _controller.Get()).Result;
            OkObjectResult listResult = searchResult.Result as OkObjectResult;

            // Assert
            Assert.Equal(200, listResult.StatusCode);
            IEnumerable<VwDeptManagerDetail> list = listResult.Value as IEnumerable<VwDeptManagerDetail>;
            Assert.Single(list);
            Assert.NotNull(list.FirstOrDefault());
            Assert.Equal(managerEmpNo, list.FirstOrDefault().EmpNo);
        }

        [Fact]
        public async void ShouldReturnDeptMangerDetailForValidDeptNo() {
            
            // Arrange
            DateTime timeNow = DateTime.Now;
            var deptManagerDetail = new VwDeptManagerDetail { 
                DeptNo = deptNo, 
                DeptName = deptName,
                FromDate = timeNow,
                ToDate = timeNow,
                EmpNo = managerEmpNo,
                FirstName = managerFirstName,
                LastName = managerLastName
            };

            mockDeptManagerDetailRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == deptNo)))
                .ReturnsAsync(deptManagerDetail);

            // Act
            ActionResult<VwDeptManagerDetail> searchResult = (await _controller.Get(deptNo));

            // Assert
            Assert.Equal(deptNo, searchResult.Value.DeptNo);
            Assert.Equal(deptName, searchResult.Value.DeptName);
            Assert.Equal(timeNow, searchResult.Value.FromDate);
            Assert.Equal(timeNow, searchResult.Value.ToDate);
            Assert.Equal(managerEmpNo, searchResult.Value.EmpNo);
            Assert.Equal(managerFirstName, searchResult.Value.FirstName);
            Assert.Equal(managerLastName, searchResult.Value.LastName);
        }

        [Fact]
        public async void ShouldUpdateDeptManagerDetailForValidDeptNo() {
            // Arrange
            DepartmentManagerDetail departmentManagerDetailUpdateRequest = 
                new DepartmentManagerDetail {
                    DeptNo = deptNo,
                    DeptName = deptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = managerEmpNo,
                    FirstName = managerFirstName,
                    LastName = managerLastName
                };

            DepartmentManagerDetail expectedUpdateResult = 
                new DepartmentManagerDetail {
                    DeptNo = newDeptNo,
                    DeptName = newDeptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = newManagerEmpNo,
                    FirstName = newManagerFirstName,
                    LastName = newManagerLastName
                };

            VwDeptManagerCurrent vwDeptManagerCurrentUpdateResult = 
                new VwDeptManagerCurrent {
                    DeptNo = newDeptNo,
                    EmpNo = newManagerEmpNo,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                };

            mockDeptManagerCurrentRepository
                .Setup(x => x.PutAsync(
                    It.Is<string>(i => i == deptNo), 
                    It.Is<VwDeptManagerCurrent>(i => 
                        i.DeptNo == departmentManagerDetailUpdateRequest.DeptNo &&
                        i.EmpNo == departmentManagerDetailUpdateRequest.EmpNo )
                ))
                .ReturnsAsync(vwDeptManagerCurrentUpdateResult);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == newManagerEmpNo.ToString())))
                .ReturnsAsync(new Employees{ 
                    EmpNo = newManagerEmpNo,
                    BirthDate = DateTime.Now,
                    FirstName = newManagerFirstName,
                    LastName = newManagerLastName
                });
            mockDepartmentsRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == newDeptNo)))
                .ReturnsAsync(new Departments {
                    DeptNo = newDeptNo,
                    DeptName = newDeptName
                });

            // Act
            ActionResult<DepartmentManagerDetail> updateResult = 
                await _controller.Put(deptNo, departmentManagerDetailUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;

            // Assert
            Assert.Null(notFoundResult);
            Assert.Equal(expectedUpdateResult.DeptNo, updateResult.Value.DeptNo);
            Assert.Equal(expectedUpdateResult.DeptName, updateResult.Value.DeptName);
            Assert.Equal(expectedUpdateResult.EmpNo, updateResult.Value.EmpNo);
            Assert.Equal(expectedUpdateResult.FirstName, updateResult.Value.FirstName);
            Assert.Equal(expectedUpdateResult.LastName, updateResult.Value.LastName);
        }

        [Fact]
        public async void ShouldReturnNotFoudWhenUpdatingDeptManagerDetailWithInvalidDeptNo() {
            // Arrange
            DepartmentManagerDetail departmentManagerDetailUpdateRequest = 
                new DepartmentManagerDetail {
                    DeptNo = invalidDeptNo,
                    DeptName = deptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = managerEmpNo,
                    FirstName = managerFirstName,
                    LastName = managerLastName
                };

            DepartmentManagerDetail expectedUpdateResult = 
                new DepartmentManagerDetail {
                    DeptNo = newDeptNo,
                    DeptName = newDeptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = newManagerEmpNo,
                    FirstName = newManagerFirstName,
                    LastName = newManagerLastName
                };

            VwDeptManagerCurrent vwDeptManagerCurrentUpdateResult = 
                new VwDeptManagerCurrent {
                    DeptNo = newDeptNo,
                    EmpNo = newManagerEmpNo,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                };

            mockDeptManagerCurrentRepository
                .Setup(x => x.PutAsync(
                    It.Is<string>(i => i == deptNo), 
                    It.Is<VwDeptManagerCurrent>(i => 
                        i.DeptNo == departmentManagerDetailUpdateRequest.DeptNo &&
                        i.EmpNo == departmentManagerDetailUpdateRequest.EmpNo )
                ))
                .ReturnsAsync(vwDeptManagerCurrentUpdateResult);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == newManagerEmpNo.ToString())))
                .ReturnsAsync(new Employees{ 
                    EmpNo = newManagerEmpNo,
                    BirthDate = DateTime.Now,
                    FirstName = newManagerFirstName,
                    LastName = newManagerLastName
                });
            mockDepartmentsRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == newDeptNo)))
                .ReturnsAsync(new Departments {
                    DeptNo = newDeptNo,
                    DeptName = newDeptName
                });

            // Act
            ActionResult<DepartmentManagerDetail> updateResult = 
                await _controller.Put(deptNo, departmentManagerDetailUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void ShouldReturnNotFoudWhenUpdatingDeptManagerDetailWithInvalidEmpNo() {
            // Arrange
            DepartmentManagerDetail departmentManagerDetailUpdateRequest = 
                new DepartmentManagerDetail {
                    DeptNo = deptNo,
                    DeptName = deptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = invalidManagerEmpNo,
                    FirstName = managerFirstName,
                    LastName = managerLastName
                };

            DepartmentManagerDetail expectedUpdateResult = 
                new DepartmentManagerDetail {
                    DeptNo = newDeptNo,
                    DeptName = newDeptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = newManagerEmpNo,
                    FirstName = newManagerFirstName,
                    LastName = newManagerLastName
                };

            VwDeptManagerCurrent vwDeptManagerCurrentUpdateResult = 
                new VwDeptManagerCurrent {
                    DeptNo = newDeptNo,
                    EmpNo = newManagerEmpNo,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                };

            mockDeptManagerCurrentRepository
                .Setup(x => x.PutAsync(
                    It.Is<string>(i => i == deptNo), 
                    It.Is<VwDeptManagerCurrent>(i => 
                        i.DeptNo == departmentManagerDetailUpdateRequest.DeptNo &&
                        i.EmpNo == departmentManagerDetailUpdateRequest.EmpNo )
                ))
                .ReturnsAsync(vwDeptManagerCurrentUpdateResult);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == newManagerEmpNo.ToString())))
                .ReturnsAsync(new Employees{ 
                    EmpNo = newManagerEmpNo,
                    BirthDate = DateTime.Now,
                    FirstName = newManagerFirstName,
                    LastName = newManagerLastName
                });
            mockDepartmentsRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == newDeptNo)))
                .ReturnsAsync(new Departments {
                    DeptNo = newDeptNo,
                    DeptName = newDeptName
                });

            // Act
            ActionResult<DepartmentManagerDetail> updateResult = 
                await _controller.Put(deptNo, departmentManagerDetailUpdateRequest);
            NotFoundResult notFoundResult = updateResult.Result as NotFoundResult;

            // Assert
            Assert.NotNull(notFoundResult);
            Assert.Equal(404, notFoundResult.StatusCode);
        }

        [Fact]
        public async void ShouldCreateDeptManagerDetailWithEmpNo() {
            // Arrange
            DepartmentManagerDetail departmentManagerDetailCreateRequest = 
                new DepartmentManagerDetail {
                    DeptNo = deptNo,
                    DeptName = deptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = managerEmpNo,
                    FirstName = managerFirstName,
                    LastName = managerLastName
                };

            DepartmentManagerDetail expectedCreateResult = 
                new DepartmentManagerDetail {
                    DeptNo = deptNo,
                    DeptName = deptName,
                    FromDate = dateTime,
                    ToDate = dateTime,
                    EmpNo = managerEmpNo,
                    FirstName = managerFirstName,
                    LastName = managerLastName
                };

            VwDeptManagerCurrent vwDeptManagerCurrentUpdateResult = 
                new VwDeptManagerCurrent {
                    DeptNo = deptNo,
                    EmpNo = managerEmpNo,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                };

            mockDeptManagerCurrentRepository
                .Setup(x => x.PostAsync(
                    It.Is<VwDeptManagerCurrent>(i => 
                        i.DeptNo == departmentManagerDetailCreateRequest.DeptNo &&
                        i.EmpNo == departmentManagerDetailCreateRequest.EmpNo )
                ))
                .ReturnsAsync(vwDeptManagerCurrentUpdateResult);
            mockEmployeeRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == managerEmpNo.ToString())))
                .ReturnsAsync(new Employees{ 
                    EmpNo = managerEmpNo,
                    BirthDate = DateTime.Now,
                    FirstName = managerFirstName,
                    LastName = managerLastName
                });
            mockDepartmentsRepository
                .Setup(x => x.GetAsync(It.Is<string>(i => i == deptNo)))
                .ReturnsAsync(new Departments {
                    DeptNo = deptNo,
                    DeptName = deptName
                });

            // Act
            ActionResult<DepartmentManagerDetail> postResult = 
                await _controller.Post(departmentManagerDetailCreateRequest);
            CreatedAtActionResult createdResult = postResult.Result as CreatedAtActionResult;
            DepartmentManagerDetail createdDeptManager = createdResult.Value as DepartmentManagerDetail;
            
            // Assert
            Assert.NotNull(createdResult);
            Assert.Equal(201, createdResult.StatusCode);
            // Assert.NotEqual(createResult.EmpNo, employeeCreateRequest.EmpNo);
            Assert.Equal(expectedCreateResult.DeptNo, createdDeptManager.DeptNo);
            Assert.Equal(expectedCreateResult.EmpNo, createdDeptManager.EmpNo);
            // Assert.Equal(expectedCreateResult.FromDate, createdDeptManager.FromDate);
            // Assert.Equal(expectedCreateResult.ToDate, createdDeptManager.ToDate);
            Assert.Equal(expectedCreateResult.DeptName, createdDeptManager.DeptName);
            Assert.Equal(expectedCreateResult.LastName, createdDeptManager.LastName);
            Assert.Equal(expectedCreateResult.FirstName, createdDeptManager.FirstName);
        }
    }
}