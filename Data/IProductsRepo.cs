using System.Collections.Generic;
using Products.Dtos;
using Products.Models;

namespace Products.Data
{
    public interface IProductsRepo
    {
        bool SaveChanges();
        ProductReadDto GetProductById(int id);
        IEnumerable<ProductReadDto> GetAllProducts();
        IEnumerable<ProductReadDto> GetProductsByQuery(string query);
        void AddProduct(Product product);
    }
}
