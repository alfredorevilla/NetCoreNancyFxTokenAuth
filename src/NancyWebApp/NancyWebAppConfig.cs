using NancyWebApp.Services;
using NancyWebApp.Services.Implementations;
using System;

namespace NancyWebApp
{
    public static class NancyWebAppConfig
    {
        public static bool AuthenticationEnabled { get; set; } = true;

        public static string Url { get; set; } = "http://localhost:5000";

        public static Func<IUserService> UserServicePerRequestInstance { get; set; } = () => new UserService();
    }
}