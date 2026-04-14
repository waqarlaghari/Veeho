using Microsoft.EntityFrameworkCore;
using Veeho.Domain.Entities;

namespace Veeho.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Video> Videos => Set<Video>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<VideoCategory> VideoCategories => Set<VideoCategory>();
        public DbSet<UserProfile> UserProfiles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoCategory>().HasKey(vc => new { vc.VideoId, vc.CategoryId });
            modelBuilder.Entity<VideoCategory>().HasOne(vc => vc.Video).WithMany().HasForeignKey(vc => vc.VideoId);
            modelBuilder.Entity<VideoCategory>().HasOne(vc => vc.Category).WithMany(c => c.VideoCategories).HasForeignKey(vc => vc.CategoryId);
            base.OnModelCreating(modelBuilder);
        }
    }
}
