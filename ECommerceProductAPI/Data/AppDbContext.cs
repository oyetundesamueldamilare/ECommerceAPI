using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ECommerceProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceProductAPI.Data
{
    public class AppDbContext  : IdentityDbContext <AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; } = null!;
    }
}
