using KalraFintech.AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace KalraFintech.AuthService.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } // This maps to table "Users"
    }
}

