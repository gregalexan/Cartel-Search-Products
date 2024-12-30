using Microsoft.EntityFrameworkCore;

namespace Cartel_Search_Products.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> User { get; set; }


    }
}
