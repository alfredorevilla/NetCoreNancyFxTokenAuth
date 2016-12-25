using IdentityModel.Client;
using System;

namespace NancyWebApp.Services
{
    public interface IIdentityServerTokenService
    {
        string GetUserId(TokenResponse token);

        DateTimeOffset GetExpirationDate(TokenResponse token);
    }
}