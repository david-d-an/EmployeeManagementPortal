using System;
using System.Collections.Generic;

namespace EMP.Data.Models
{
    public partial class VwEmpDetails
    {
        public int EmpNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTime HireDate { get; set; }
        public string Gender { get; set; }
        public int Salary { get; set; }
        public string Title { get; set; }
        public string DeptName { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public int ManagerEmpNo { get; set; }
    }
}
