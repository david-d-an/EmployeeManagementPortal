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
        public static IEnumerable<ApiResource> GetApiResources(IConfiguration cfg, ILogger<Startup> logger)
        {
            var apiResource = new ApiResource(cfg["ApiId"], cfg["ApiName"]);
            logger.LogInformation(string.Format("{0}: {1}", "API Name", apiResource.Name));
            logger.LogInformation(string.Format("{0}: {1}", "API DisplayName", apiResource.DisplayName));

            // new ApiResource("projects-api", "Projects API")
            return new List<ApiResource> { apiResource };
        }

        private static List<string> GetConfigSection(IConfiguration cfg, string sectionName) {
            List<string> section = new List<string>();
            foreach (var i in cfg.GetSection(sectionName).GetChildren()) {
                section.Add(i.Value);
            }
            return section;
        }

        public static IEnumerable<Client> GetClients(IConfiguration cfg, ILogger<Startup> logger)
        {
            var RedirectUris = GetConfigSection(cfg, "RedirectUris");
            var PostLogoutRedirectUris = GetConfigSection(cfg, "PostLogoutRedirectUris");
            var AllowedCorsOrigins = GetConfigSection(cfg, "AllowedCorsOrigins");

            var client = new Client {
                #region
                // ClientId = "emp-web-client",
                // ClientName = "emp-web-client",
                // RequireClientSecret = false,
                // AllowedGrantTypes = GrantTypes.Code,
                // RequirePkce = true,
                // AllowAccessTokensViaBrowser = true,
                // RequireConsent = false,

                // RedirectUris = { 
                //     "http://localhost:5000/signin-callback", 
                //     "http://localhost:5000/assets/silent-callback.html" 
                // },
                // PostLogoutRedirectUris = { 
                //     "http://localhost:5000/signout-callback" 
                // },
                // AllowedCorsOrigins = { 
                //     "http://localhost:5000",
                //     "https://localhost:5001",
                //     "http://ipv4.fiddler:5000",
                //     "https://ipv4.fiddler:5001"
                // },
                // AllowedScopes =
                // {
                //     IdentityServerConstants.StandardScopes.OpenId,
                //     IdentityServerConstants.StandardScopes.Profile,
                //     "projects-api"
                // },
                #endregion
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
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    cfg["ApiId"]
                },
                AccessTokenLifetime = 600
            };

            logger.LogInformation(string.Format("{0}: {1}", "ClientId", client.ClientId));
            logger.LogInformation(string.Format("{0}: {1}", "ClientName", client.ClientName));
            logger.LogInformation(string.Format("{0}: {1}", "RequirePkce", client.RequirePkce));

            foreach(var s in client.RedirectUris) {
                logger.LogInformation(string.Format("{0}: {1}", "RedirectUris", s));
            }
            foreach(var s in client.PostLogoutRedirectUris) {
                logger.LogInformation(string.Format("{0}: {1}", "PostLogoutRedirectUris", s));                
            }
            foreach(var s in client.AllowedCorsOrigins) {
                logger.LogInformation(string.Format("{0}: {1}", "AllowedCorsOrigins", s));                    
            }

            return new List<Client> { client };
        }

        public static string GetPublicOrigin(IConfiguration cfg, ILogger<Startup> logger)
        {
            var publicOrigin = cfg["PublicOrigin"];
            logger.LogInformation(string.Format("{0}: {1}", "PublicOrigin", publicOrigin));                    
            return publicOrigin;
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
    }
}