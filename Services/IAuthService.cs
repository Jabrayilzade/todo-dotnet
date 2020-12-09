using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoWish.Models;

namespace TodoWish.Services
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(User user, string password);
        Task<User> LoginAsync(string email, string password);
        Task<bool> UserExistsAsync(string email);
    }
}
