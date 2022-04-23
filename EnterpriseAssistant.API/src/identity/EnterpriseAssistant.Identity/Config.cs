using System.Collections.Generic;
using IdentityServer4;
using IdentityServer4.Models;

namespace EnterpriseAssistant.Identity
{
    public static class Config
    {
        private const string ApiUrl = "https://localhost:5002";

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new("ea", "Enterprise Assistant")
        };

        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new("ea")
        };

        public static IEnumerable<Client> Clients => new Client[]
        {
            new()
            {
                ClientId = "ea.api",
                ClientName = "Enterprise Assistant",
                ClientSecrets = {new Secret("ea.secret".Sha256())},
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = false,
                RedirectUris = {$"{ApiUrl}/signin-oidc"},
                PostLogoutRedirectUris = {$"{ApiUrl}/signout-callback-oidc"},
                AllowedCorsOrigins = {$"{ApiUrl}"},
                AllowOfflineAccess = true,

                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "ea",
                },
            }
        };
    }
}