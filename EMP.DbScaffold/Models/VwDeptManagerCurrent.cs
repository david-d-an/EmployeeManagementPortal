using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models
{
    public partial class VwDeptManagerCurrent
    {
        public int EmpNo { get; set; }
        public string DeptNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
    }
}
