using System;
using System.Collections.Generic;

namespace EMP.Data.Models.Employees
{
    public partial class DeptManagerCurrent
    {
        public int EmpNo { get; set; }
        public string DeptNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
