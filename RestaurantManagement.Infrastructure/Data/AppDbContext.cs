using Microsoft.EntityFrameworkCore;
using RestaurantManagement.Domain.Entities;

namespace RestaurantManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // v1
        // public DbSet<Restaurant> Restaurants { get; set; }

        // status category
        public DbSet<StatusCategory> StatusCategories { get; set; }
        // public DbSet<Status> Status { get; set; }
        //public DbSet<Role> Roles { get; set; }
        // public DbSet<User> Users { get; set; }
        // public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Room> Rooms { get; set; }
        // public DbSet<Table> Tables { get; set; }
        // public DbSet<Category> Categories { get; set; }
        // public DbSet<Dish> Dishes { get; set; }
        // public DbSet<Order> Orders { get; set; }
        // public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<TableSession> TableSessions { get; set; }
        public DbSet<TableStatusType> TableStatusTypes { get; set; }
        public DbSet<OrderStatusType> OrderStatusTypes { get; set; }
        public DbSet<DishStatusType> DishStatusTypes { get; set; }

        // v2
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Restaurants> Restaurants { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Dishes> Dishes { get; set; }
        public DbSet<Tables> Tables { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // v1
            modelBuilder.Entity<StatusCategory>().ToTable("StatusCategories");
            // modelBuilder.Entity<Status>().ToTable("Statuses");
            // modelBuilder.Entity<Restaurant>().ToTable("Restaurants");
            //modelBuilder.Entity<Role>().ToTable("Roles");
            // modelBuilder.Entity<User>().ToTable("Users");
            // modelBuilder.Entity<RefreshToken>().ToTable("RefreshTokens");
            modelBuilder.Entity<Room>().ToTable("Rooms");
            // modelBuilder.Entity<Table>().ToTable("Tables");
            // modelBuilder.Entity<Category>().ToTable("Categories");
            // modelBuilder.Entity<Dish>().ToTable("Dishes");
            // modelBuilder.Entity<Order>().ToTable("Orders");
            // modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
            modelBuilder.Entity<TableSession>().ToTable("TableSessions");
            modelBuilder.Entity<TableStatusType>().ToTable("TableStatusTypes");
            modelBuilder.Entity<OrderStatusType>().ToTable("OrderStatusTypes");
            modelBuilder.Entity<DishStatusType>().ToTable("DishStatusTypes");

            // v2
            modelBuilder.Entity<Roles>().ToTable("roles");
            modelBuilder.Entity<Status>().ToTable("status");
            modelBuilder.Entity<Voucher>().ToTable("vouchers");
            modelBuilder.Entity<Restaurants>().ToTable("Restaurants");
            modelBuilder.Entity<Users>().ToTable("Users");
            modelBuilder.Entity<Categories>().ToTable("Categories");
            modelBuilder.Entity<Dishes>().ToTable("Dishes");
            modelBuilder.Entity<Tables>().ToTable("Tables");
            modelBuilder.Entity<Orders>().ToTable("Orders");
            modelBuilder.Entity<OrderDetails>().ToTable("order_details");

            base.OnModelCreating(modelBuilder);

        }
    }
}
