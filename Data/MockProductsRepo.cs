using Products.Models;
using System;
using System.Collections.Generic;
namespace Products.Data
{
    public class MockProductsRepo : IProductsRepo
    {

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product> {
            new Product {Id= 1,Name="Amul Milk", Category= "Milk", Brand= "Amul", Description="cow milk", Price=26, SellerId=1, },
            new Product {Id= 2,Name="Dairy Milk", Category= "Milk", Brand= "Dairy", Description="cow milk", Price=26, SellerId=1, },
            new Product {Id= 3,Name="Mother Milk", Category= "Milk", Brand= "Mother", Description="cow milk", Price=26, SellerId=1, },
            };

            return products;
        }

        public Product GetProductById(int id)
        {
            return new Product { Id = 1, Name = "Amul Milk", Category = "Milk", Brand = "Amul", Description = "cow milk", Price = 26, SellerId = 1, };
        }

        public IEnumerable<Product> GetProductsByQuery(string query)
        {
            throw new NotImplementedException();
        }

        public void AddProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
