using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // v1
        public DbSet<Restaurant> Restaurants { get; set; }

        // status category
        public DbSet<StatusCategory> StatusCategories { get; set; }
        public DbSet<Status> Status { get; set; }
        //public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<TableSession> TableSessions { get; set; }
        public DbSet<TableStatusType> TableStatusTypes { get; set; }
        public DbSet<OrderStatusType> OrderStatusTypes { get; set; }
        public DbSet<DishStatusType> DishStatusTypes { get; set; }
        
        // v2
        public DbSet<Roles> Roles { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // v1
            modelBuilder.Entity<StatusCategory>().ToTable("StatusCategories");
            modelBuilder.Entity<Status>().ToTable("Statuses");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            //modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            modelBuilder.Entity<Table>().ToTable("Tables");
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Dish>().ToTable("Dishes");
            modelBuilder.Entity<Order>().ToTable("Orders");
            modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
            modelBuilder.Entity<TableSession>().ToTable("TableSessions");
            modelBuilder.Entity<TableStatusType>().ToTable("TableStatusTypes");
            modelBuilder.Entity<OrderStatusType>().ToTable("OrderStatusTypes");
            modelBuilder.Entity<DishStatusType>().ToTable("DishStatusTypes");

            // v2
            modelBuilder.Entity<Roles>().ToTable("roles");


            base.OnModelCreating(modelBuilder);

        }
    }
}
