using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace EMP.Sts
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("projects-api", "Projects API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "emp-web-client",
                    ClientName = "emp-web-client",
                    RequireClientSecret = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    RequireConsent = false,

                    RedirectUris = { 
                        "http://localhost:5000/signin-callback", 
                        "http://localhost:5000/assets/silent-callback.html" 
                    },
                    PostLogoutRedirectUris = { 
                        "http://localhost:5000/signout-callback" 
                    },
                    AllowedCorsOrigins = { 
                        "http://localhost:5000",
                        "https://localhost:5001",
                        "http://ipv4.fiddler:5000",
                        "https://ipv4.fiddler:5001"
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "projects-api"
                    },
                    AccessTokenLifetime = 600
                },
                // new Client
                // {
                //     ClientId = "mvc",
                //     ClientName = "MVC Client",
                //     AllowedGrantTypes = GrantTypes.Hybrid,

                //     ClientSecrets =
                //     {
                //         new Secret("secret".Sha256())
                //     },

                //     RedirectUris           = { "http://localhost:4201/signin-oidc" },
                //     PostLogoutRedirectUris = { "http://localhost:4201/signout-callback-oidc" },

                //     AllowedScopes =
                //     {
                //         IdentityServerConstants.StandardScopes.OpenId,
                //         IdentityServerConstants.StandardScopes.Profile
                //     },
                //     AllowOfflineAccess = true

                // }
            };

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