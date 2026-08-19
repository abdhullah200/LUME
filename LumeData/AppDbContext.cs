using Lume.Data.Models;
using LumeData.Models;
using Microsoft.EntityFrameworkCore;

namespace Lume.Data
{
    /// <summary>
    /// Represents the application's database context for managing posts and users.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDbContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        /// <summary>
        /// Gets or sets the posts table.
        /// </summary>
        public DbSet<Post> Posts { get; set; }

        /// <summary>
        /// Gets or sets the users table.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Configures the model relationships and constraints for the database context.
        /// </summary>
        /// <param name="modelBuilder">The model builder used to configure the entity models.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the relationship between Post and User
            modelBuilder.Entity<User>()
                .HasMany(u => u.Posts)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId);
        }
    }
}
