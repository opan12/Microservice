using Microsoft.EntityFrameworkCore;

namespace Microservice.user.api
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
    }



}