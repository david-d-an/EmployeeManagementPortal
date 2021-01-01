using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models
{
    public partial class DeptEmpLatestDate
    {
        public int EmpNo { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
