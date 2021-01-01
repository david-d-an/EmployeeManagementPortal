using System;
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
    public class DeptManagerControllerTest
    {
        private int empNo;
        private int invalidEmpNo;
        private string deptNo;
        private DeptManager deptManager;
        private Mock<ILogger<DeptManagerController>> mockLogger;
        private Mock<IDeptManagerRepository> mockDeptManagerRepository;
        private DeptManagerController _controller;

        public DeptManagerControllerTest()
        {
            empNo = 0;
            invalidEmpNo = -1;
            deptNo = "0";

            deptManager = 
                new DeptManager {
                    EmpNo = empNo,
                    DeptNo = deptNo,
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now
                 };

            mockLogger = new Mock<ILogger<DeptManagerController>>();

            mockDeptManagerRepository = new Mock<IDeptManagerRepository>();
            mockDeptManagerRepository.Setup(x => x.GetAsync(empNo)).ReturnsAsync(deptManager);
            mockDeptManagerRepository.Setup(x => x.GetAsync(invalidEmpNo)).ReturnsAsync((DeptManager)null);

            _controller = new DeptManagerController(
                mockLogger.Object,
                mockDeptManagerRepository.Object);            
        }

        [Fact]
        public void ShouldReturnAllDeptManagers() {
            // Arrange
            var deptManagers = new List<DeptManager> {
                deptManager
            };

            mockDeptManagerRepository.Setup(x => x.GetAsync()).ReturnsAsync(deptManagers);

            // Act
            Task<IEnumerable<DeptManager>> searchResult = _controller.Get();

            // Assert
            Assert.NotNull(searchResult.Result);
            Assert.Single(searchResult.Result);
            Assert.NotNull(searchResult.Result.FirstOrDefault());
            Assert.Equal(searchResult.Result.FirstOrDefault().EmpNo, empNo);
        }

        [Fact]
        public void ShouldReturnDeptManagerWithEmpNo() {
            // Arrange

            // Act
            Task<DeptManager> searchResult = _controller.Get(empNo);

            // Assert
            Assert.Equal(searchResult.Result.EmpNo, empNo);
        }

        [Fact]
        public void ShouldReturnNoDeptManagerWithInvalidEmpNo() {
            // Arrange

            // Act
            Task<DeptManager> searchResult = _controller.Get(invalidEmpNo);

            // Assert
            Assert.Null(searchResult.Result);
        }
    }
}
