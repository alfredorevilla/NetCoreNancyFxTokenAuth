using NancyWebApp.Models;
using System.Collections.Generic;

namespace NancyWebApp.Services
{
    public interface IUserService
    {
        IEnumerable<Book> GetUserFavoriteBooks(string userId);

        IEnumerable<User> GetUsers();
    }
}