using AuthService.Domain.Entities;
using AuthService.Domain.Interfaces;

namespace AuthService.Infrastructure.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
            {
                new User 
                { 
                    Id = 1,
                    Username = "admin", 
                    Password = "admin123", 
                    Role = "Admin",
                    Email = "admin@example.com",
                    FullName = "Administrator",
                    CreatedAt = DateTime.UtcNow.AddDays(-30),
                    IsDeleted = false
                },
                new User 
                { 
                    Id = 2,
                    Username = "user", 
                    Password = "user123", 
                    Role = "User",
                    Email = "user@example.com",
                    FullName = "Regular User",
                    CreatedAt = DateTime.UtcNow.AddDays(-15),
                    IsDeleted = false
                },
                new User 
                { 
                    Id = 3,
                    Username = "manager", 
                    Password = "manager123", 
                    Role = "Manager",
                    Email = "manager@example.com",
                    FullName = "Manager User",
                    CreatedAt = DateTime.UtcNow.AddDays(-7),
                    IsDeleted = false
                }
            };
        }

        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            await Task.Delay(1); // Simulate async operation
            return _users.FirstOrDefault(u => u.Username == username && !u.IsDeleted);
        }

        public async Task<User?> ValidateUserAsync(string username, string password)
        {
            await Task.Delay(1); // Simulate async operation
            return _users.FirstOrDefault(u => u.Username == username && u.Password == password && !u.IsDeleted);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await Task.Delay(1); // Simulate async operation
            user.Id = _users.Max(u => u.Id) + 1;
            user.CreatedAt = DateTime.UtcNow;
            user.IsDeleted = false;
            _users.Add(user);
            return user;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            return _users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            await Task.Delay(1); // Simulate async operation
            return _users.Where(u => !u.IsDeleted).ToList();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            await Task.Delay(1); // Simulate async operation
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.Username = user.Username;
                existingUser.Password = user.Password;
                existingUser.Role = user.Role;
                existingUser.Email = user.Email;
                existingUser.FullName = user.FullName;
                existingUser.UpdatedAt = DateTime.UtcNow;
            }
            return user;
        }

        public async Task DeleteUserAsync(int id)
        {
            await Task.Delay(1); // Simulate async operation
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                user.IsDeleted = true;
                user.UpdatedAt = DateTime.UtcNow;
            }
        }
    }
}
