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
        /// Gets or sets the likes table.
        /// </summary>
        public DbSet<Like> Likes { get; set; }

        /// <summary>
        /// Gets or sets the comments table.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }

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

            modelBuilder.Entity<Like>()
                .HasKey(l => new { l.postId, l.userId });

            modelBuilder.Entity<Like>()
                .HasOne(l => l.post)
                .WithMany(p => p.likes)
                .HasForeignKey(l => l.postId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Like>()
                .HasOne(l => l.user)
                .WithMany(u => u.Likes)
                .HasForeignKey(l => l.userId)
                .OnDelete(DeleteBehavior.Restrict);

            // Comments
            modelBuilder.Entity<Comment>()
                .HasOne(l => l.post)
                .WithMany(p => p.comments)
                .HasForeignKey(l => l.postId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Comment>()
                .HasOne(l => l.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(l => l.userId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
