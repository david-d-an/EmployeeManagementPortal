#region
// using System.Collections.Generic;
// using IdentityServer4;
// using IdentityServer4.Models;

// namespace EMP.Sts
// {
//     public class Config1
//     {
//         public static IEnumerable<ApiResource> GetApiResources()
//         {
//             return new List<ApiResource>
//             {
//                 new ApiResource("projects-api", "Projects API")
//             };
//         }

//         public static IEnumerable<Client> GetClients()
//         {
//             return new List<Client>
//             {
//                 new Client
//                 {
//                     ClientId = "spa-client",
//                     ClientName = "Projects SPA",
//                     RequireClientSecret = false,
//                     AllowedGrantTypes = GrantTypes.Code,
//                     RequirePkce = true,
//                     AllowAccessTokensViaBrowser = true,
//                     RequireConsent = false,


//                     RedirectUris =           { "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
//                     PostLogoutRedirectUris = { "http://localhost:4200/signout-callback" },
//                     AllowedCorsOrigins =     { "http://localhost:4200" },

//                     AllowedScopes =
//                     {
//                         IdentityServerConstants.StandardScopes.OpenId,
//                         IdentityServerConstants.StandardScopes.Profile,
//                         "projects-api"
//                     },
//                     AccessTokenLifetime = 600
//                 },
//                 new Client
//                 {
//                     ClientId = "mvc",
//                     ClientName = "MVC Client",
//                     AllowedGrantTypes = GrantTypes.Hybrid,

//                     ClientSecrets =
//                     {
//                         new Secret("secret".Sha256())
//                     },

//                     RedirectUris           = { "http://localhost:4201/signin-oidc" },
//                     PostLogoutRedirectUris = { "http://localhost:4201/signout-callback-oidc" },

//                     AllowedScopes =
//                     {
//                         IdentityServerConstants.StandardScopes.OpenId,
//                         IdentityServerConstants.StandardScopes.Profile
//                     },
//                     AllowOfflineAccess = true

//                 }
//             };

//         }

//         public static IEnumerable<IdentityResource> GetIdentityResources()
//         {
//             return new List<IdentityResource>
//             {
//                 new IdentityResources.OpenId(),
//                 new IdentityResources.Profile(),
//             };
//         }
//     }
// }
#endregion


using System.Collections.Generic;
using System.Configuration;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace EMP.Sts
{
    public class Config
    {
        public static string GetRsaKeyLocation(IConfiguration cfg) {
            return cfg["RsaKeyLocation"];
        }

        public static IEnumerable<ApiResource> GetApiResources(IConfiguration cfg)
        {
            var apiResource = new ApiResource(cfg["ApiId"], cfg["ApiName"]);
            // logger.LogInformation(string.Format("{0}: {1}", "API Name", apiResource.Name));
            // logger.LogInformation(string.Format("{0}: {1}", "API DisplayName", apiResource.DisplayName));
            return new List<ApiResource> { apiResource };
        }

        private static List<string> GetConfigSection(IConfiguration cfg, string sectionName) {
            List<string> section = new List<string>();
            foreach (var i in cfg.GetSection(sectionName).GetChildren()) {
                section.Add(i.Value);
            }
            return section;
        }

        public static int GetCookieExpirationByMinute(IConfiguration cfg) {
            int cookieExpiration = 0;
            int.TryParse(cfg["CookieExpirationByMinute"], out cookieExpiration);
            return cookieExpiration;
        }

        public static IEnumerable<Client> GetClients(IConfiguration cfg) {
            var RedirectUris = GetConfigSection(cfg, "RedirectUris");
            var PostLogoutRedirectUris = GetConfigSection(cfg, "PostLogoutRedirectUris");
            var AllowedCorsOrigins = GetConfigSection(cfg, "AllowedCorsOrigins");
            var cookieExpiration = GetCookieExpirationByMinute(cfg);

            var client = new Client {
                ClientId = cfg["ClientId"],
                ClientName = cfg["ClientId"],
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = cfg.GetValue<bool>("RequirePkce"),
                AllowAccessTokensViaBrowser = true,
                RequireConsent = false,
                RedirectUris = GetConfigSection(cfg, "RedirectUris"),
                PostLogoutRedirectUris = GetConfigSection(cfg, "PostLogoutRedirectUris"),
                AllowedCorsOrigins = GetConfigSection(cfg, "AllowedCorsOrigins"),
                AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    cfg["ApiId"]
                },
                
                RefreshTokenUsage = TokenUsage.OneTimeOnly,

                AbsoluteRefreshTokenLifetime = 60 * 60 * 24,  //60 * 60 *24, // 24 hours
                SlidingRefreshTokenLifetime = 60 * 60 * 1, // 15 mins
                RefreshTokenExpiration = TokenExpiration.Sliding,

                // Token refreshes 60 seconds earlier than AccessTokenLifetime
                // To prevent Access Token from overriding Cookie refresh, add extra 10 seconds as safety. 
                AccessTokenLifetime = 60 * cookieExpiration + 10,
            };

            // logger.LogInformation(string.Format("{0}: {1}", "ClientId", client.ClientId));
            // logger.LogInformation(string.Format("{0}: {1}", "ClientName", client.ClientName));
            // logger.LogInformation(string.Format("{0}: {1}", "RequirePkce", client.RequirePkce));

            // foreach(var s in client.RedirectUris) {
            //     logger.LogInformation(string.Format("{0}: {1}", "RedirectUris", s));
            // }
            // foreach(var s in client.PostLogoutRedirectUris) {
            //     logger.LogInformation(string.Format("{0}: {1}", "PostLogoutRedirectUris", s));                
            // }
            // foreach(var s in client.AllowedCorsOrigins) {
            //     logger.LogInformation(string.Format("{0}: {1}", "AllowedCorsOrigins", s));                    
            // }

            return new List<Client> { client };
        }

        public static string GetPublicOrigin(IConfiguration cfg) {
            var publicOrigin = cfg["PublicOrigin"];
            // logger.LogInformation(string.Format("{0}: {1}", "PublicOrigin", publicOrigin));                    
            return publicOrigin;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources() {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}