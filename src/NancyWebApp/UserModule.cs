using Nancy;
using NancyWebApp.Services;

namespace NancyWebApp
{
    public class UserModule : NancyModule
    {
        public UserModule(IUserService userService) : base("/api/")
        {
            this.UserService = userService;

            //  post /api/users
            this.Post("/users/", o =>
            {
                //this.RequiresAuthentication();
                return this.UserService.GetUsers();
            });

            //  post /api/user/{id}/books
            this.Post("/user/{id}/books", o => new[] { "Dune", "Star Wars" });
        }

        private IUserService UserService { get; }
    }
}