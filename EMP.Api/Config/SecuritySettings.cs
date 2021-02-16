using System.Collections.Generic;

namespace EMP.Api.Config
{
    public class SecuritySettings
    {
        public string ApiName { get; set; }
        public string StsAuthority { get; set; }
        public ICollection<string> AllowedCorsOrigins { get; set; }
    }
}