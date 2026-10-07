using Microsoft.EntityFrameworkCore;
using Balofinos_Midterm_Store.Models;

namespace Balofinos_Midterm_Store.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed 5 Initial Products
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Wireless Mouse", Description = "Ergonomic 2.4GHz wireless optical mouse", Price = 19.99m, Category = "Electronics" },
                new Product { Id = 2, Name = "Mechanical Keyboard", Description = "RGB backlit mechanical gaming keyboard", Price = 59.99m, Category = "Electronics" },
                new Product { Id = 3, Name = "HD Monitor 24\"", Description = "1080p Full HD IPS display monitor", Price = 129.50m, Category = "Electronics" },
                new Product { Id = 4, Name = "Ceramic Coffee Mug", Description = "15oz insulated coffee mug", Price = 9.99m, Category = "Home & Kitchen" },
                new Product { Id = 5, Name = "Travel Backpack", Description = "Water-resistant laptop travel backpack", Price = 39.99m, Category = "Accessories" }
            );
        }
    }
}