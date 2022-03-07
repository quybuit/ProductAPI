using Microsoft.EntityFrameworkCore;
using ProductAPI.Core;

namespace ProductAPI.Infrastructure.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public ProductContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(c => new { c.Id });
        }
    }
}
