﻿using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace JediOrder.Web {
   public static class Config {
      public static IEnumerable<IdentityResource> IdentityResources =>
         new List<IdentityResource>
         {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
         };

      public static IEnumerable<ApiScope> ApiScopes =>
         new List<ApiScope>
         {
            new ApiScope("api1", "My API" )
         };
      public static IEnumerable<Client> Clients =>
         new List<Client>
         {
            new Client
            {
               ClientId = "client",
               ClientSecrets = { new Secret("secret".Sha256()) },
               AllowedGrantTypes = GrantTypes.ClientCredentials,
               AllowedScopes = { "api1" }
            },
            //new Client
            //{
            //   ClientId = "ro.client",
            //   ClientSecrets = { new Secret("secret".Sha256()) },
            //   AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            //   AllowedScopes = { "api1" }
            //},
            new Client
            {
               ClientId = "mvc",
               ClientSecrets = { new Secret("secret".Sha256()) },
               AllowedGrantTypes = GrantTypes.Code,
               RedirectUris = { "https://localhost/signin-oidc" },
               PostLogoutRedirectUris = { "https://localhost/signout-callback-oidc" },
               AllowedScopes =
               {
                  IdentityServerConstants.StandardScopes.OpenId,
                  IdentityServerConstants.StandardScopes.Profile,
                  "api1"
               }
            }
         };
   }
}
