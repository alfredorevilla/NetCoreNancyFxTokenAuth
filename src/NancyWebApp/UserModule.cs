using Nancy;
using NancyWebApp.Models;
using System.Collections.Generic;

namespace NancyWebApp
{
    public class UserModule : NancyModule
    {
        public UserModule()
        {
            this.Get<IEnumerable<User>>("/nancy/users/", o => new[] { new User { Id = "arevilla", UserName = "arevilla" } }, null, "GetUsers");
        }
    }
}