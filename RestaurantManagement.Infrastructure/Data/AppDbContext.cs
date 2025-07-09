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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StatusCategory>().ToTable("StatusCategories");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");

            base.OnModelCreating(modelBuilder);

        }

        //     // status
        //     public DbSet<Status> Status { get; set; }
        //     protected override void OnModelCreating(ModelBuilder modelBuilder)
        //     {
        //         modelBuilder.Entity<Status>().ToTable("Status");
        //     }
    }
}
