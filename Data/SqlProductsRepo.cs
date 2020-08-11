using Products.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Products.Data
{
    public class SqlProductsRepo : IProductsRepo
    {
        private readonly ProductsContext _context;
        public SqlProductsRepo(ProductsContext context)
        {
            _context = context;
        }
        public void AddProduct(Product product)
        {
            //No need to yet
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Product> GetProductsByQuery(string query)
        {
            throw new NotImplementedException();
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
