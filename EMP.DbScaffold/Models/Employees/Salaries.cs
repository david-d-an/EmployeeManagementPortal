using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models.Employees
{
    public partial class Salaries
    {
        public long Id { get; set; }
        public int EmpNo { get; set; }
        public int Salary { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual Employees EmpNoNavigation { get; set; }
    }
}
