using NancyWebApp.IdentityServer;
using NancyWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace NancyWebApp.Services.Implementations
{
    public class UserService : IUserService
    {
        public IEnumerable<Book> GetUserFavoriteBooks(string userId)
        {
            return new[] {
                new Book {
                    Author = "Frank Miller",
                    Title = "The Dark Knight Returns",
                    Category = "Graphic Novel" } ,
                new Book {
                    Author = "Frank Herbert",
                    Title = "Dune",
                    Category = "Science Fiction" } };
        }

        public IEnumerable<User> GetUsers()
        {
            return
               IdentityServerConfig.GetUsers().Select(o => new User
               {
                   Email = o.Claims.Single(claim => claim.Type == "email").Value,
                   Id = o.Subject,
                   UserName = o.Username
               });
        }
    }
}