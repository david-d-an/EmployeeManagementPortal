using EMP.Data.Models;

namespace EMP.Data.Models
{
    public class EmployeeDetail : Employees
    {
        public string DeptNo { get; set; }
        public string DeptName { get; set; }
        public int ManagerEmpNo { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public int Salary { get; set; }
        public string Title { get; set; }
    }
}