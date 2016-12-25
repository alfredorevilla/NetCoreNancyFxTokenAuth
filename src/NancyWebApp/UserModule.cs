using Nancy;
using Nancy.Security;
using NancyWebApp.Services;

namespace NancyWebApp
{
    public class UserModule : NancyModule
    {
        public UserModule(IUserService userService) : base("/api/")
        {
            this.UserService = userService;

            //  require auth for all module endpoints
            this.RequiresAuthentication();

            //  post /api/users
            this.Post("/users/", o =>
            {
                return this.UserService.GetUsers();
            });

            //  post /api/user/{id}/books
            this.Post("/user/{id}/books", o =>
            {
                return this.UserService.GetUserFavoriteBooks(o.id);
            });
        }

        private IUserService UserService { get; }
    }
}