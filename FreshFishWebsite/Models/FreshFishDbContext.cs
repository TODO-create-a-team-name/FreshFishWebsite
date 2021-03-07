using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreshFishWebsite.Models
{
    public class FreshFishDbContext : IdentityDbContext<User>
    {
        public FreshFishDbContext(DbContextOptions<FreshFishDbContext> options) : base(options) {}

        public DbSet<StorageAdmin> StorageAdmins { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ShoppingCartProduct> ShoppingCartProducts { get; set; }
        public DbSet<Storage> Storages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(s => s.ShoppingCart)
                .WithOne(u => u.User)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ShoppingCart>()
                .HasMany(s => s.Products)
                .WithOne(s => s.ShoppingCart)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
