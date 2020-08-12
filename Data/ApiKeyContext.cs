using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Data
{
    public class ApiKeyContext : DbContext
    {
        public ApiKeyContext(DbContextOptions<ProductsContext> opt) : base(opt)
        {
        }
        public DbSet<ApiKey> ApiKeys { get; set; }
    }
}
