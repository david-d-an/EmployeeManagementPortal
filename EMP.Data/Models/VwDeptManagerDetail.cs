using System;
using System.Collections.Generic;

namespace EMP.Data.Models
{
    public partial class VwDeptManagerDetail
    {
        public string DeptNo { get; set; }
        public string DeptName { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int EmpNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
