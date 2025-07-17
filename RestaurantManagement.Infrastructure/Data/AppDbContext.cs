using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Restaurant> Restaurants { get; set; }

        // status category
        public DbSet<StatusCategory> StatusCategories { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusCategory>().ToTable("StatusCategories");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Table>().ToTable("Tables");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Dish>().ToTable("Dishes");

            base.OnModelCreating(modelBuilder);

        }
    }
}
