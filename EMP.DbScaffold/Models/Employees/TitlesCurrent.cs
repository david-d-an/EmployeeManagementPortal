using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models.Employees
{
    public partial class TitlesCurrent
    {
        public int EmpNo { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
