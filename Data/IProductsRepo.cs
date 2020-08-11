using System.Collections.Generic;
using Products.Models;

namespace Products.Data
{
    public interface IProductsRepo
    {
        bool SaveChanges();
        Product GetProductById(int id);
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Product> GetProductsByQuery(string query);
        void AddProduct(Product product);
    }
}
