using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> opt) : base(opt)
        {
        }
        public DbSet<Product> Products { get; set; }
    }
}
