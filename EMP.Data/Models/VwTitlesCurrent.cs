using System;
using System.Collections.Generic;

namespace EMP.Data.Models
{
    public partial class VwTitlesCurrent
    {
        public int EmpNo { get; set; }
        public string Title { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
