using IdentityModel.Client;
using System;

namespace NancyWebApp.Services.Implementations
{
    public class IdentityServerTokenService : IIdentityServerTokenService
    {
        public DateTimeOffset GetExpirationDate(TokenResponse token)
        {
            throw new NotImplementedException();
        }

        public string GetUserId(TokenResponse token)
        {
            throw new NotImplementedException();
        }
    }
}