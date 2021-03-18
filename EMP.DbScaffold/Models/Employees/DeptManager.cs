﻿using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models.Employees
{
    public partial class DeptManager
    {
        public long Id { get; set; }
        public int EmpNo { get; set; }
        public string DeptNo { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public virtual Departments DeptNoNavigation { get; set; }
        public virtual Employees EmpNoNavigation { get; set; }
    }
}
