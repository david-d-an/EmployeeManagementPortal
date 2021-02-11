using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models.Employees
{
    public partial class VwEmpDetailsShort
    {
        public int EmpNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public int? Salary { get; set; }
        public string DeptNo { get; set; }
        public string DeptName { get; set; }
    }
}
