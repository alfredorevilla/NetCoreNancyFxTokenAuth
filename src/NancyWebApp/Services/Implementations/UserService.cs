using NancyWebApp.Models;
using System.Collections.Generic;

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
            return new[] {
                new User {
                    Email = "alfredorevilla@gmail.com",
                    Id = "1",
                    UserName = "alfredorevilla" },
            new User {
                    Email = "johndoe@microsoft.com",
                    Id = "2",
                    UserName = "johndoe" }};
        }
    }
}