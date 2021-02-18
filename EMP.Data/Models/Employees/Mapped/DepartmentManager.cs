using System;

namespace EMP.Data.Models.Employees.Mapped
{
    public class DepartmentManagerDetail
    {
        public string DeptNo { get; set; }
        public string DeptName { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? EmpNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}