using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;
using System.Collections.Generic;

namespace AuthService.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;
        private readonly Dictionary<string, string> _blackListToken;

        public UserRepository()
        {
            _users = new List<User>
            {
                new() { Username = "admin", Password = "admin123", Role = "Admin" },
                new() { Username = "user", Password = "user123", Role = "User" }
            };

            _blackListToken = new Dictionary<string, string>();
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            await Task.Delay(1);
            return _users.FirstOrDefault(u => u.Username == username);
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            await Task.Delay(1);
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password);
        }
    }
}
