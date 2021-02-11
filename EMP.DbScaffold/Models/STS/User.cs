using System;
using System.Collections.Generic;

namespace EMP.DbScaffold.Models.STS
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string HashedPassword { get; set; }
        public bool? Locked { get; set; }
    }
}
