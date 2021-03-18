using System;
using System.Collections.Generic;

namespace EMP.Data.Models.Employees
{
    public partial class Titles
    {
        public long Id { get; set; }
        public int EmpNo { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual Employees EmpNoNavigation { get; set; }
    }
}
