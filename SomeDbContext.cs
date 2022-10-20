using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo
{
    public class SomeDbContext : DbContext
    {
        public SomeDbContext(DbContextOptions<SomeDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAddress> Addresses { get; set; }
    }
}
