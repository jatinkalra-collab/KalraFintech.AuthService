using KalraFintech.AuthService.Data;
using KalraFintech.AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace KalraFintech.AuthService.Repositories
{
    public class UserRepository
    {
        private readonly AuthDbContext _dbContext;

        public UserRepository(AuthDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new user
        public async Task<User> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        // Get a user by username
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
