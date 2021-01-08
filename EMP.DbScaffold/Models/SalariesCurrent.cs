using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models
{
    public partial class SalariesCurrent
    {
        public int EmpNo { get; set; }
        public int Salary { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
