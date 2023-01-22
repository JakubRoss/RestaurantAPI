using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Role>()
                .Property(x=>x.Name)
                .IsRequired()
                .HasMaxLength(50);
            modelBuilder.Entity<Restaurant>()
                .Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Address>()
                .Property(n=>n.City) 
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Address>()
                .Property(n => n.Street)
                .IsRequired()
                .HasMaxLength(25);
            modelBuilder.Entity<Dish>()
                .Property(n=>n.Name)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}