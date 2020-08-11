using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Products.Models;

namespace Products.Dtos
{
    public class ProductReadDto
    {
        [Key]
        public int Id { get; set; }
        [Required, MinLength(3)]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required, MinLength(3)]
        public string Category { get; set; }
        [Required]
        public double Price { get; set; }
        [Required, MinLength(2)]
        public string Brand { get; set; }

        public ProductReadDto(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            Description = product.Description;
            Brand = product.Brand;
            Price = product.Price;
            Category = product.Category;
        }
    }
}
