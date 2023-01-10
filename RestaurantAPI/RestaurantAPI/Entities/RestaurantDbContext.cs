using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.Entities
{
    public class RestaurantDbContext : DbContext
    {
        public RestaurantDbContext(DbContextOptions<RestaurantDbContext> contextOptions) : base(contextOptions)
        {

        }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Adress> Adresses { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>()
                .Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(25);

            modelBuilder.Entity<Dish>()
                .Property(n=>n.Name)
                .IsRequired()
                .HasMaxLength(25);
        }
    }
}