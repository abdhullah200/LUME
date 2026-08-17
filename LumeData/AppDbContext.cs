using Lume.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Lume.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; } 
    }
}
