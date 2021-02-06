using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FreshFishWebsite.Models
{
    public class FreshFishDbContext : IdentityDbContext<User>
    {
        public FreshFishDbContext(DbContextOptions<FreshFishDbContext> options) : base(options) {}

        public DbSet<Product> Products { get; set; }


    }
}
