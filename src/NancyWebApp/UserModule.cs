using Nancy;
using Nancy.Security;
using NancyWebApp.Models;
using System.Collections.Generic;

namespace NancyWebApp
{
    public class UserModule : NancyModule
    {
        public UserModule()
        {
            this.RequiresAuthentication();
            this.Get<IEnumerable<User>>("/nancy/users/", o => new[] { new User { Id = "arevilla", UserName = "arevilla" } }, null, "GetUsers");
        }
    }
}