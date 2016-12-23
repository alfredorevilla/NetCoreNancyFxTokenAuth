using IdentityServer4.Models;
using System.Collections.Generic;

namespace NancyWebApp.IdentityServer
{
    public class Config
    {
        public static string[] GetServerAddresses()
        {
            return new[] { "http://localhost:5000" };
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