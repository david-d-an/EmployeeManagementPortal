using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models.Sts
{
    public partial class AspnetDeptManager
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string DeptNo { get; set; }

        public virtual Aspnetusers User { get; set; }
    }
}
