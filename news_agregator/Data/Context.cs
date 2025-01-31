using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;

namespace Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
        : base(options)
        {}
    
        public DbSet<User> Users { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<NewsArticle> NewsArticles { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Preference> Preferences { get; set; }
        public DbSet<SavedArticle> SavedArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Preference)
                .WithOne(p => p.User)
                .HasForeignKey<Preference>(p => p.UserId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.SavedArticles)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithOne(n => n.User)
                .HasForeignKey(n => n.UserId);
            modelBuilder.Entity<NewsArticle>()
                .HasMany(n => n.Comments)
                .WithOne(c => c.NewsArticle)
                .HasForeignKey(c => c.ArticleId);
        }
    }
}

