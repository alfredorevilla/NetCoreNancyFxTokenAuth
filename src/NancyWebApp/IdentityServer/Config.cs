using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services.InMemory;
using System.Collections.Generic;
using System.Security.Claims;

namespace NancyWebApp.IdentityServer
{
    public class Config
    {
        public static List<InMemoryUser> GetUsers()
        {
            return new List<InMemoryUser> {
            new InMemoryUser {
                Subject = "F7D5AC0B-5C00-4253-8B54-046392260658",
                Username = "alfredorevilla",
                Password = "password",
                Claims = new List<Claim> {
                    new Claim(JwtClaimTypes.Email, "alfredorevilla@gmail.com"),
                    new Claim(JwtClaimTypes.Role, "Administrator"),
                    new Claim(JwtClaimTypes.Role, "Developer"),
                }
            }
        };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("api1", "My API") };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>{
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                }
              };
        }
    }
}