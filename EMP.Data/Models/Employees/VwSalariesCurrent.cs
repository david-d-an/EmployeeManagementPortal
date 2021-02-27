using System;
using System.Collections.Generic;

namespace EMP.Data.Models.Employees
{
    public partial class VwSalariesCurrent
    {
        public int EmpNo { get; set; }
        public int? Salary { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
