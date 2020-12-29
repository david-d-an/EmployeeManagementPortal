using System;
using EMP.Data.Models;
using Moq;
using Xunit;
using EMP.Core.Repos;
using EMP.Core.Processors;
using System.Collections.Generic;
using System.Linq;

namespace EMP.Core
{
    public class DeptManagerProcessorTest
    {
        private int empNo;
        private int invalidEmpNo;
        private string deptNo;
        private DeptManager deptManager;
        private Mock<IDeptManagerRepository> mockDeptManagerRepository;
        private DeptManagerProcessor _processor;

        public DeptManagerProcessorTest()
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

            mockDeptManagerRepository = new Mock<IDeptManagerRepository>();
            mockDeptManagerRepository.Setup(x => x.Get(empNo)).Returns(deptManager);
            mockDeptManagerRepository.Setup(x => x.Get(invalidEmpNo)).Returns<DeptManager>(null);

            _processor = new DeptManagerProcessor(mockDeptManagerRepository.Object);            
        }

        [Fact]
        public void ShouldReturnAllDeptManagers() {
            // Arrange
            var deptManagers = new List<DeptManager> {
                deptManager
            };

            mockDeptManagerRepository.Setup(x => x.Get()).Returns(deptManagers);

            // Act
            IEnumerable<DeptManager> searchResult = _processor.GetAllDeptManagers();

            // Assert
            Assert.NotNull(searchResult);
            Assert.Single(searchResult);
            Assert.NotNull(searchResult.FirstOrDefault());
            Assert.Equal(searchResult.FirstOrDefault().EmpNo, empNo);
        }

        [Fact]
        public void ShouldReturnDeptManagerWithEmpNo() {
            // Arrange

            // Act
            DeptManager searchResult = _processor.GetDeptManagerByEmpNo(empNo);

            // Assert
            Assert.Equal(searchResult.EmpNo, empNo);
        }

        [Fact]
        public void ShouldReturnNoDeptManagerWithInvalidEmpNo() {
            // Arrange

            // Act
            DeptManager searchResult = _processor.GetDeptManagerByEmpNo(invalidEmpNo);

            // Assert
            Assert.Null(searchResult);
        }
    }
}
